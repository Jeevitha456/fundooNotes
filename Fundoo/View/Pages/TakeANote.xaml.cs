// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TakeANote.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------

namespace Fundoo.View.Pages
{
    using System;
    using Fundoo.Interface;
    using Fundoo.Firebase;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Take A Note
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class TakeANote : ContentPage
    {
        /// <summary>
        /// The firebase helper
        /// </summary>
        public FirebaseHelper firebaseHelper = new FirebaseHelper();

        /// <summary>
        /// Initializes a new instance of the <see cref="TakeANote"/> class.
       /// </summary>
        public TakeANote()
        {
            this.InitializeComponent();
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

        /// <summary>
        /// Handles the Clicked event of the Button Add control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            try
            {
                FirebaseHelper firebaseHelper = new FirebaseHelper();
                await this.firebaseHelper.AddNote(txtTitle.Text, txtNotes.Text);
                //// Empty all user input after the data
                txtTitle.Text = string.Empty;
                txtNotes.Text = string.Empty;

                //// If it is successfull displays mesaage
                await this.DisplayAlert("Success", "Notes added successfully", "ok");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Handles the Clicked event of the text control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TxtArchieve_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ArchievePage());
        }
    }
}