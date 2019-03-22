using Fundoo.Interface;
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
	public partial class SignOut : ContentPage
	{
		public SignOut ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            DependencyService.Get<IFirebaseAuthenticator>().SignOut();
             Navigation.PushModalAsync(new Login());

        }
    }
}