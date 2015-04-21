#Xamarin Plugins

Xamarin Plugins are a special kind of nuget package which let you easily add cross-platform functionality to your apps. Using nuget to distribute Plugins means that anyone can create them and, of course, consume them. The different platforms supported by Plugins include Android, iOS (64bit + Unified API), Windows Phone 8, Windows Phone 8.1 RT and Windows Store.

Plugins are small, have few or no dependencies, and provide a mechanism for accessing a single device function. The following table represents some of the features you would expect to find on modern mobile devices. The aim of a Plugin is to expose one such feature using a singleton object, accessed through an Interface that abstracts the platform specific implementation on all platforms.  

This approach in synonymous with using Inversion of Control ([IoC]()) and the [Service Locator]() pattern to invoke platform specific code from within a shared project or a portable class library ([PCL]()).

|:-------------:|:-------------:|:-------------:|
|**OS Level** |||||
| Media | Speech To Text | App Settings | System Info
| **Hardware Level** |||||
| GPS | Network | Battery | Vibrate

> Plugins abstract common device functionality or expose cross-platform APIs via a single interface

##Plugins aren't Components

######*But they can be by following the Component creation and publishing guidelines [here](http://en.wikipedia.org/wiki/Service_locator_pattern)*

Components typically differ from Plugins in two main ways,  

* they tend to be used to offer cross-platform functionality at the UI level
* and they deeply integrate into each platform they target

These differences usually result in diverging API implementations on each platform. In fact very few Components target iOS, Android and Windows. Plugins, on the other hand, strive for a true cross-platform offering; through one common API.

##Plugin Development & Deployment

Plugins can be created by anyone and are a third-party driven part of the Xamarin ecosystem. Plugins do not need to be reviewed or approved by Xamarin.  

In order to build a Plugin you will need Visual Studio and a business license of Xamarin (30 day trial to start). Publishing will require and a nuget user account. If you want to create and support your Plugins but have no intention to build and distribute apps, then you can apply for a special free license of Xamarin.  

Finally, you must publish your Plugin under a friendly OSS license like [MIT.](http://en.wikipedia.org/wiki/MIT_License)

##Example

Many Plugins have already been published. Simply browse the nuget packages repository at [nuget.org.](https://www.nuget.org/packages?q=plugin+for+xamarin) Alternatively you can see the Plugins offered in the Xamarin Component store [here.]()

###Cross App-Version

It is common in almost every app to show the current version. Each OS platform uses a different approach to storing and retrieving this information. Abstracting this function with a Plugin makes sense.  



