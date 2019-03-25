using Fundoo.Firebase;
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
	public partial class TakeANote : ContentPage
	{
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public TakeANote ()
		{
			InitializeComponent ();
		}

       
        /*
        protected  override bool OnBackButtonPressed()
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();
           // var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
             this.firebaseHelper.AddNote(txtTitle.Text,txtNotes.Text);

            //// Empty all user input after the data
            txtTitle.Text = string.Empty;
            txtNotes.Text = string.Empty;
         

           //// If it is successfull displays mesaage
            this.DisplayAlert("Success", "Signed-up successfully", "ok");
            base.OnBackButtonPressed;
            return false;
        }
        */
        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            try
            {
                FirebaseHelper firebaseHelper = new FirebaseHelper();
                // var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
                await this.firebaseHelper.AddNote(txtTitle.Text, txtNotes.Text);

                //// Empty all user input after the data
                txtTitle.Text = string.Empty;
                txtNotes.Text = string.Empty;


                //// If it is successfull displays mesaage
                await this.DisplayAlert("Success", "Notes added" +" successfully", "ok");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void TxtArchieve_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ArchievePage());
        }
    }
}