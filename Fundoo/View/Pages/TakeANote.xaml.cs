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
    using Fundoo.Model;
    using global::Firebase.Database;
    using global::Firebase.Database.Query;
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
        private FirebaseClient firebase = new FirebaseClient("https://fundooapp-50c31.firebaseio.com/");
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
                this.firebaseHelper.AddNote(txtTitle.Text, txtNotes.Text);
               
                //// If it is successfull displays mesaage
                this.DisplayAlert("Success", "Notes added successfully", "ok");
                base.OnBackButtonPressed();
              
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
          
            return false;
            

           
        }



        /// <summary>
        /// Handles the Clicked event of the Text control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TxtArchieve_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ArchievePage());
        }
    }
}