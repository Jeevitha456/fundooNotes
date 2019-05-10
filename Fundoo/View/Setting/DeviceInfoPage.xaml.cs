using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fundoo.View.Setting
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DeviceInfoPage : ContentPage
	{
		public DeviceInfoPage ()
		{
			InitializeComponent ();
            // Device Model (SMG-950U, iPhone10,6)
          txtDeviceModel.Text ="Model : "+ DeviceInfo.Model;

            // Manufacturer (Samsung)
           txtDeviceManufacturer.Text ="Manufacturer : " + DeviceInfo.Manufacturer;

            // Device Name (Motz's iPhone)
            txtDeviceName.Text ="Name : " + DeviceInfo.Name;

            // Operating System Version Number (7.0)
           txtDeviceVersion.Text = "Version : " + DeviceInfo.VersionString;

            // Platform (Android)
            txtDevicePlatform.Text = "Platform : " + DeviceInfo.Platform;

            // Idiom (Phone)
          txtDeviceIdiom.Text = "Idiom : " + DeviceInfo.Idiom;

            // Device Type (Physical)
            txtDeviceType.Text ="Type : " + $"{DeviceInfo.DeviceType}";
        }
	}
}