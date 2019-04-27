using Fundoo.Firebase;
using Fundoo.Interface;
using Fundoo.Model;
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
	public partial class UpdateLabels : ContentPage
	{
        string value = null;
		public UpdateLabels(string labelKey)
		{
            value = labelKey;
			InitializeComponent ();
            EditLabel();
		}

        public async void EditLabel()
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            //// Gets current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            ////Gets the notes data
            CreateNewLabel createNewLabel = await firebaseHelper.GetLabelsData(this.value,userid);

            txtLabel.Text = createNewLabel.Label;
          
        }

        protected override bool OnBackButtonPressed()
        {
            try
            {
                FirebaseHelper firebaseHelper = new FirebaseHelper();

                //// Gets current user id
                var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

                //// Updates the notes whenUpdateNotes method is called
                CreateNewLabel label = new CreateNewLabel()
                {
                    Label=txtLabel.Text,
                  
                };
                firebaseHelper.UpdateLabels(label, this.value, userid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return base.OnBackButtonPressed();
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            //// Gets current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            //// Updates the notes when DeleteNotes method is called
            CreateNewLabel label = new CreateNewLabel()
            {
                Label=txtLabel.Text,
            };
            firebaseHelper.DeleteLabel(label, this.value, userid);
        }
    }
}