using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fundoo.View.Setting
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingPage : ContentPage
	{
		public SettingPage ()
		{
			InitializeComponent ();
		}

        private void TxtAppInfo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AppInfoPage());
        }

        private void TxtDeviceInfo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DeviceInfoPage());
        }

        private void TxtVersionTracking_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VersionTrackingPage());
        }

        private void TxtBrowser_Clicked(object sender, EventArgs e)
        {
            Xamarin.Essentials.Browser.OpenAsync("https://firebase.google.com/", 
                Xamarin.Essentials.BrowserLaunchMode.SystemPreferred);
        }

        private void TxtOrientationSensor_Clicked(object sender, EventArgs e)
        {
           
        }
    }
}
