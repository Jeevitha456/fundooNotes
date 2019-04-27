// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TakeANote.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------

namespace Fundoo.View.Pages
{
    using System;
    using global::Firebase.Database;
    using Fundoo.Firebase;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Rg.Plugins.Popup.Services;
    using Fundoo.Model;
    using System.Collections.Generic;

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
        /// Firebase Client
        /// firebase
        /// </summary>
        private FirebaseClient firebase = new FirebaseClient("https://fundooapp-50c31.firebaseio.com/");

        /// <summary>
        /// Initializes a new instance of the <see cref="TakeANote"/> class.
        /// </summary>
        public TakeANote()
        {
            this.InitializeComponent();
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

                //// Adds notes to the firebase
                ///
                NotesData notes = new NotesData()
                {
                    Title=txtTitle.Text,
                    Notes=txtNotes.Text,
                    ColorNote=noteColor,
                    LabelData=new List<string>()
                };
                this.firebaseHelper.AddNote(notes);
               
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

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopTaskView());
        }

        public Color ColorNotes { get; set; }
        private string noteColor = "White";
        private void RedButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Crimson;
            this.noteColor = "Red";
        }

        private void OrangeButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Orange;
            this.noteColor = "Orange";
        }

        private void YellowButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Yellow;
            this.noteColor = "Yellow";
        }

        private void GreenButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.PaleGreen;
            this.noteColor = "Green";
        }

        private void BlueButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.LightSkyBlue;
            this.noteColor = "Blue";
        }

        private void TealButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Aquamarine;
            this.noteColor = "Teal";
        }

        private void DarkBlueButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.CornflowerBlue;
            this.noteColor = "DarkBlue";
        }

        private void PurpleButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.LightSteelBlue;
            this.noteColor = "Purple";
        }

        private void PinkButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Pink;
            this.noteColor = "Pink";
        }

        private void BrownButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.RosyBrown;
            this.noteColor = "Brown";
        }

        private void GrayButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.LightGray;
            this.noteColor = "Gray";
        }
    }
}