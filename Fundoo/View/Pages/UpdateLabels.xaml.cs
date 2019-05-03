// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateLabels.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{
    using System;
    using Fundoo.Firebase;
    using Fundoo.Interface;
    using Fundoo.Model;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Update Labels class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateLabels : ContentPage
    {
        /// <summary>
        /// The value
        /// </summary>
        private string value = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateLabels"/> class.
        /// </summary>
        /// <param name="labelKey">The label key.</param>
        public UpdateLabels(string labelKey)
        {
            this.value = labelKey;
            this.InitializeComponent();
            this.EditLabel();
        }

        /// <summary>
        /// Edits the label.
        /// </summary>
        public async void EditLabel()
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            //// Gets current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            ////Gets the notes data
            CreateNewLabel createNewLabel = await firebaseHelper.GetLabelsData(this.value, userid);

            txtLabel.Text = createNewLabel.Label;         
        }

        /// <summary>
        /// Application developers can override this method to provide behavior when the back button is pressed.
        /// </summary>
        /// <returns>
        /// To be added.
        /// </returns>
        /// <remarks>
        /// To be added.
        /// </remarks>
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
                    Label = txtLabel.Text,                 
                };
                firebaseHelper.UpdateLabels(label, this.value, userid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return base.OnBackButtonPressed();
        }

        /// <summary>
        /// Handles the Clicked event of the Delete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Delete_Clicked(object sender, EventArgs e)
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            //// Gets current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            //// Updates the notes when DeleteNotes method is called
            CreateNewLabel label = new CreateNewLabel()
            {
                Label = txtLabel.Text,
            };
            firebaseHelper.DeleteLabel(label, this.value, userid);
        }
    }
}