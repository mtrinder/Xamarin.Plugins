#Xamarin Plugins

Xamarin Plugins are a special kind of NuGet package which let you easily add cross-platform functionality to your apps. Using NuGet to distribute Plugins means that anyone can create them, and of course consume them. The platforms supported by Plugins include Android, iOS, iOS Unified (64bit), Windows Phone 8, Windows Phone 8.1 RT and Windows Store.

> Plugins abstract common device functionality or expose cross-platform APIs via a single interface

Plugins are small, have few or no dependencies, and provide a simple mechanism for accessing a single device function. The following table represents some of the features you would expect to find on modern mobile devices.  

| OS Level ||||
|:------------- |:------------- |:------------- |:------------- |:------------- |
| Media | Speech To Text | App Settings | System Info
| **Hardware Level** ||||
| GPS | Network | Battery | Vibrate

The aim of a Plugin is to expose one such feature using an object singleton, accessed through an Interface that abstracts the platform specific implementation on all platforms. This approach in synonymous with using Inversion of Control ([IoC]()) and the [Service Locator]() pattern to invoke platform specific code from within a shared project or a Portable Class Library ([PCL]()).

##Plugins Aren't Components

> But they can be by following the Component creation and publishing [guidelines](http://en.wikipedia.org/wiki/Service_locator_pattern).

Components typically differ from Plugins in two main ways,  

* they tend to be used to offer cross-platform functionality at the UI level
* and they deeply integrate into each platform they target

These differences usually result in diverging API implementations on each platform. As a consequence the use of the API becomes tightly coupled to each platform. In addition, very few Components target all of iOS, Android and Windows. Plugins, on the other hand, strive for a true cross-platform offering through one common API.

##Plugin Development & Deployment

Plugins can be created by anyone and are a third-party driven part of the Xamarin ecosystem. Plugins do not need to be reviewed or approved by Xamarin.  

In order to build a Plugin you will need Visual Studio and a business license of Xamarin (use a fully featured 30 day trial if you're new to Xamarin). Publishing will require a NuGet [user account](https://www.nuget.org/users/account/LogOn?returnUrl=%2Fusers%2Faccount%2F). If you want to create and support your Plugins but have no intention to build and distribute apps, then you can apply for a special free license of Xamarin.  

Finally, you must publish your Plugin under an Open Source (OSS) friendly license like [MIT](http://en.wikipedia.org/wiki/MIT_License)

##Walkthrough

Many Plugins have already been published so definitely checkout the packages repository at [nuget.org](https://www.nuget.org/packages?q=plugin+for+xamarin) just in case your Plugin idea has already been developed. Alternatively you can view the Plugins offered in the [Xamarin Component store]().

###Cross-platform "Version"

It's common in almost every app to show the current version. Each platform uses a different approach for storing and retrieving this information, so abstracting this simple function with a Plugin makes sense.  

####Quick Setup

* Check your NuGet [Package Manager](https://visualstudiogallery.msdn.microsoft.com/27077b70-9dad-4c64-adcf-c7cf6bc9970c) is up to date. Ensure version 2.8.3
* Download and Install the [Xamarin Plugin Template](https://visualstudiogallery.msdn.microsoft.com/afead421-3fbf-489a-a4e8-4a244ecdbb1e) for Visual Studio
* Create a project using the new Plugin template. The name of the solution needs to be simple because it will used throughout the Plugin code structure as a prefix and postfix for classes and file names.

![](ScreenShot1.png)

With all projects created and initialized the screen shot below shows what your solution structure should resemble. Each platform being targeted has an corresponding project, with two additional portable (PCL) projects needed to complete the Plugin architecture.

![](ScreenShot2.png)

The project called Version.Plugin.Abstractions is where we need to define the common API Interface. If you expand this project's file structure you will see that an empty Interface has already been created for you by the template. For this walkthtough our Interface is called IVersion.  

At a minimum this Plugin needs to return the app's version as a string, so we'll add a getter property to our API.

    /// <summary>
    /// Interface for Version
    /// </summary>
    public interface IVersion
    {
        /// <summary>
        /// Current App Version
        /// </summary>
        string Version { get; }
    }

That's it. With our API defined we just need to create concrete implementations for each targeted platform. When the template created each platform specific project, it added an implementation class with the name of your Plugin as the prefix.

**IVersion for iOS Implementation**

    ///
    /// ./Version.Plugin.iOS/VersionImplementation.cs
    ///
    public class VersionImplementation : IVersion
    {
      readonly NSString _buildKey;
      readonly NSString _versionKey;
    
      public VersionImplementation()
      {
          _buildKey = new NSString("CFBundleVersion");
          _versionKey = new NSString("CFBundleShortVersionString");
      }
    
      /// <summary>
      /// Returns the app version with build number appended
      /// </summary>
      public string Version
      {
          get
          {
              var build = NSBundle.MainBundle.InfoDictionary.ValueForKey(_buildKey);
              var version = NSBundle.MainBundle.InfoDictionary.ValueForKey(_versionKey);
              return string.Format("{0}.{1}", version, build);
          }
      }
    }

**IVersion for Android Implementation**

    ///
    /// ./Version.Plugin.Android/VersionImplementation.cs
    ///
    public class VersionImplementation : IVersion
    {
      /// <summary>
      /// Returns the app version
      /// </summary>
      public string Version
      {
          get { return GetPackageVersion(); }
      }
    
      static string GetPackageVersion()
      {
          try
          {
              return Application.Context.PackageManager.GetPackageInfo(
                  Application.Context.PackageName, PackageInfoFlags.MetaData).VersionName;
          }
          catch
          {
              return string.Empty;
          }
      }
    }

The other platform implementations can be found in the sample code along with apps for testing each one.

####CrossVersion Class

Using the API is the same on each platform.

    var version = CrossVersion.Current.Version;
    
    Debug.WriteLine(version);

As you can see the name of the class that exposes the API has the Plugin name as a postfix. This is the standard naming convention for accessing all Xamarin Plugins. A cross-platform Plugin for "battery status" would be called with a CrossBattery class.

The CrossVersion class can be found in the first portable class in solution explorer - Version.Plugin. You may have noticed this class file is also linked into all of the platform specific projects. This simple yet powerful class ensures the correct IVersion implementation is loaded at runtime because it's compiled into each assembly.

    public class CrossVersion
    {
        static readonly Lazy<IVersion> Implementation = new Lazy<IVersion>(() =>
            #if PORTABLE
                null,
            #else
                new VersionImplementation(),
            #endif
              System.Threading.LazyThreadSafetyMode.PublicationOnly);
    
        public static IVersion Current
        {
            get
            {
                var version = Implementation.Value;
                if (version == null)
                {
                    throw new NotImplementedException("Don't use CrossVersion from your PCL without first installing the NuGet package in your main project first.");
                }
                return version;
            }
        }
    }

"Current" is a lazy loaded static object of type VersionImplementation, exposed via IVersion.

##NuGet Deployment

