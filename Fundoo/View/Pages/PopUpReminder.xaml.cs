using Plugin.InputKit.Shared.Controls;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fundoo.View.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUpReminder : PopupPage
    {
		public PopUpReminder ()
		{
			InitializeComponent ();
		}
        static readonly Random rnd = new Random();


        private void RandomizeColors(object sender, EventArgs e)
        {
            var colors = typeof(Color).GetFields();
            var color = (Color)colors[rnd.Next(colors.Length)].GetValue(null);
            foreach (var view in group.Children)
            {
                if (view is RadioButton rb)
                {
                    rb.Color = color;
                }
            }
        }

        private void PlaceClicked_Clicked(object sender, EventArgs e)
        {
            
           
            Navigation.PushModalAsync(new GeoLocation());
            PopupNavigation.Instance.PopAsync(true);

        }
    }
}