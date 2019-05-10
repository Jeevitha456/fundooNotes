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
	public partial class VersionTrackingPage : ContentPage
	{
		public VersionTrackingPage ()
		{
			InitializeComponent ();
            try
            {
                // First time ever launched application
            // var firstLaunch = "First Launch : " + VersionTracking.IsFirstLaunchEver;

                // First time launching current version
                txtfirstLaunchCurrent.Text = "First Launch for current version : " + $"{ VersionTracking.IsFirstLaunchForCurrentVersion}";

                // First time launching current build
                txtfirstLaunchBuild.Text = "First Launch For Current Build : " + $"{VersionTracking.IsFirstLaunchForCurrentBuild}";

                // Current app version (2.0.0)
                txtcurrentVersion.Text = "Current Version : " + $"{VersionTracking.CurrentVersion}";

                // Current build (2)
                txtcurrentBuild.Text = "Current Build : " + $"{VersionTracking.CurrentBuild}";

                // Previous app version (1.0.0)
                txtpreviousVersion.Text = "Previous Version" + VersionTracking.PreviousVersion;

                // Previous app build (1)
                txtpreviousBuild.Text = "Previous Build : " + VersionTracking.PreviousBuild;

                // First version of app installed (1.0.0)
                txtfirstVersion.Text = "First Version : " + VersionTracking.FirstInstalledVersion;

                // First build of app installed (1)
                txtfirstBuild.Text = "First Installed : " + VersionTracking.FirstInstalledBuild;

                // List of versions installed (1.0.0, 2.0.0)
                txtversionHistory.Text = "Version History : " + $"{ VersionTracking.VersionHistory}";

                // List of builds installed (1, 2)
                txtbuildHistory.Text = "Build History : " + $"{VersionTracking.BuildHistory}";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
         
        }
	}
}