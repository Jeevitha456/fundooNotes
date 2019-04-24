// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateNote.xaml.cs" company="Bridgelabz">
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
    using Fundoo.View.HomePage;
    using Rg.Plugins.Popup.Services;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Update Note
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateNote : ContentPage
    {
        /// <summary
        /// >
        /// The value
        /// </summary>
        private string val = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNote"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public UpdateNote(string value)
        {
            this.val = value;
            this.InitializeComponent();
            this.UpdateData();
        }

        /// <summary>
        /// Updates the data.
        /// </summary>
        public async void UpdateData()
        {
            try
            {
                FirebaseHelper firebaseHelper = new FirebaseHelper();

                //// Gets current user id
                var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
               
                ////Gets the notes data
                NotesData notesData = await firebaseHelper.GetNotesData(this.val, userid);

                txtTitle.Text = notesData.Title;
                txtNotes.Text = notesData.Notes;
                this.noteColor = notesData.ColorNote;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }       
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
                NotesData notes = new NotesData()
                {
                    Title = txtTitle.Text,
                    Notes = txtNotes.Text,
                    ColorNote = this.noteColor
                };
                firebaseHelper.UpdateNotes(notes, this.val, userid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return base.OnBackButtonPressed(); 
        }

        /// <summary>
        /// Handles the Clicked event of the ImageButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopTaskView(this.val));
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
            NotesData notes = new NotesData()
            {
                Title = txtTitle.Text,
                Notes = txtNotes.Text,
                ColorNote = this.noteColor,
                IsDeleted = true
            };
            firebaseHelper.DeleteNotes(notes, this.val, userid);
        }

        /// <summary>
        /// Handles the Clicked event of the control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TxtArchieve_Clicked(object sender, EventArgs e)
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            //// Gets current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            //// Updates the notes when ArchiveNotes method is called
            NotesData notes = new NotesData()
            {
                Title = txtTitle.Text,
                Notes = txtNotes.Text,
                IsArchive = true,
                ColorNote = this.noteColor
            };
            firebaseHelper.ArchiveNotes(notes, this.val, userid);
        }

        /// <summary>
        /// Handles the Clicked event of the TxtPin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TxtPin_Clicked(object sender, EventArgs e)
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            //// Gets current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            //// Updates the notes when ArchiveNotes method is called
            NotesData notes = new NotesData()
            {
                Title = txtTitle.Text,
                Notes = txtNotes.Text,
               IsPinned = true,
                ColorNote = this.noteColor
            };
            firebaseHelper.PinnedNotes(notes, this.val, userid);
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

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopPlus());
        }
    }
}