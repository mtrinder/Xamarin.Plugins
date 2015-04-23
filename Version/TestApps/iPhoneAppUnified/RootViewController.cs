using System;
using System.Drawing;
using CoreGraphics;
using Foundation;
using UIKit;
using Version.Plugin;

namespace iPhoneAppUnified
{
    public partial class RootViewController : UIViewController
    {
        public RootViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        UILabel _versionLabel;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _versionLabel = new UILabel(CGRect.Empty);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (_versionLabel.Frame == CGRect.Empty)
            {
                _versionLabel.Frame = View.Frame;
                _versionLabel.Center = View.Center;
                _versionLabel.TextAlignment = UITextAlignment.Center;
                _versionLabel.Font = UIFont.SystemFontOfSize(32f);

                Add(_versionLabel);
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            _versionLabel.Text = string.Format("Version: {0}", CrossVersion.Current.Version);
        }
    }
}