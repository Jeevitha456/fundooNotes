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
	public partial class AppInfoPage : ContentPage
	{
		public AppInfoPage ()
		{
			InitializeComponent ();
            txtAppName.Text="App Name : " + $"{ AppInfo.Name}";

            // Package Name/Application Identifier (com.microsoft.testapp)
          txtPakageName.Text ="Package : "+$"{ AppInfo.PackageName}";

            // Application Version (1.0.0)
           txtVersion.Text ="Version : " + $"{AppInfo.VersionString}";

            // Application Build Number (1)
          txtBuild.Text="Build : " +$"{AppInfo.BuildString}";
        }
	}
}