// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PopTaskView.xaml.cs" company="Bridgelabz">
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
    using Rg.Plugins.Popup.Pages;
    using Rg.Plugins.Popup.Services;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Pop Task View
    /// </summary>
    /// <seealso cref="Rg.Plugins.Popup.Pages.PopupPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopTaskView : PopupPage
    {
        /// <summary>
        /// The value
        /// </summary>
        private string value = null;

        /// <summary>
        /// The notes
        /// </summary>
       private NotesData notes = null;

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
        /// Initializes a new instance of the <see cref="PopTaskView"/> class.
        /// </summary>
        public PopTaskView()
        {          
          this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PopTaskView"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="notesData">The notes data.</param>
        public PopTaskView(string key, NotesData notesData)
        {
            this.notes = notesData;
            this.value = key;
          this.InitializeComponent();
        }

        /// <summary>
        /// Handles the 1 event of the Button_Clicked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            //// Navigate sto label page
            Navigation.PushModalAsync(new Labels(this.value, this.notes));
            PopupNavigation.Instance.PopAsync(true);
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            //// Gets the current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            NotesData notesData = await firebaseHelper.GetNotesData(this.value, userid);
            await Share.RequestAsync(new ShareTextRequest
            {              
               Text = notesData.Notes,
               Title = "Share Text"
            });
          await PopupNavigation.Instance.PopAsync(true);
        }

        /// <summary>
        /// Reds the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RedButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Red;
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
            this.BackgroundColor = Color.Green;
            this.noteColor = "Green";
        }

        /// <summary>
        /// Blues the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BlueButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Blue;
            this.noteColor = "Blue";
        }

        /// <summary>
        /// Teals the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TealButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Teal;
            this.noteColor = "Teal";
        }

        /// <summary>
        /// Darks the blue button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DarkBlueButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.DarkBlue;
            this.noteColor = "DarkBlue";
        }

        /// <summary>
        /// Purples the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PurpleButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Purple;
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
            this.BackgroundColor = Color.Brown;
            this.noteColor = "Brown";
        }

        /// <summary>
        /// Grays the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GrayButton(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Gray;
            this.noteColor = "Gray";
        }
    }
}