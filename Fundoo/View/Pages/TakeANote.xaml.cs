// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TakeANote.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{
    using System;
    using System.Collections.Generic;
    using global::Firebase.Database;
    using Fundoo.Firebase; 
    using Fundoo.Model;
    using Rg.Plugins.Popup.Services;
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
                NotesData notes = new NotesData()
                {
                    Title = txtTitle.Text,
                    Notes = txtNotes.Text,
                    ColorNote = this.noteColor,
                    LabelData = new List<string>()
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

        /// <summary>
        /// Handles the Clicked event of the ImageButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopTaskView());
        }

        /// <summary>
        /// Gets or sets the color notes.
        /// </summary>
        /// <value>
        /// The color notes.
        /// </value>
        public Color ColorNotes { get; set; }

        /// <summary>
        /// The note color
        /// </summary>
        private string noteColor = "White";

        /// <summary>
        /// Reds the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RedButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Crimson;
            this.noteColor = "Red";
        }

        /// <summary>
        /// Oranges the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OrangeButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Orange;
            this.noteColor = "Orange";
        }

        /// <summary>
        /// Yellows the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void YellowButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Yellow;
            this.noteColor = "Yellow";
        }

        /// <summary>
        /// Greens the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GreenButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.PaleGreen;
            this.noteColor = "Green";
        }

        /// <summary>
        /// Blues the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BlueButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.LightSkyBlue;
            this.noteColor = "Blue";
        }

        /// <summary>
        /// Teals the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TealButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Aquamarine;
            this.noteColor = "Teal";
        }

        /// <summary>
        /// Darks the blue button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DarkBlueButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.CornflowerBlue;
            this.noteColor = "DarkBlue";
        }

        /// <summary>
        /// Purples the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PurpleButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.LightSteelBlue;
            this.noteColor = "Purple";
        }

        /// <summary>
        /// Pinks the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PinkButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Pink;
            this.noteColor = "Pink";
        }

        /// <summary>
        /// Browns the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BrownButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.RosyBrown;
            this.noteColor = "Brown";
        }

        /// <summary>
        /// Grays the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GrayButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.LightGray;
            this.noteColor = "Gray";
        }
    }
}