// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TakeANote.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------

namespace Fundoo.View.Pages
{
    using System;
    using Fundoo.Firebase;
    using Fundoo.Interface;
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
        private FirebaseHelper firebaseHelper = new FirebaseHelper();

        /// <summary>
        /// Initializes a new instance of the <see cref="TakeANote"/> class.
        /// </summary>
        public TakeANote()
        {
            this.InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();
            this.firebaseHelper.AddNote(txtTitle.Text, txtNotes.Text);
            //// Empty all user input after the data
            txtTitle.Text = string.Empty;
            txtNotes.Text = string.Empty;

            //// If it is successfull displays mesaage
            this.DisplayAlert("Success", "Notes added successfully", "ok");
            base.OnBackButtonPressed();
            return false;
        }

        private void TxtArchieve_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ArchievePage());
        }
    }
}