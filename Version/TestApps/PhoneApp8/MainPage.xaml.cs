using System;
using System.Linq;
using Microsoft.Phone.Controls;
using Version.Plugin;

namespace PhoneApp8
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            VersionTextBlock.Text = string.Format("Version: {0}", CrossVersion.Current.Version);
        }
    }
}