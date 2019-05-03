// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdatePinNotes.xaml.cs" company="Bridgelabz">
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
    using Rg.Plugins.Popup.Services;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Update Pin Notes
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatePinNotes : ContentPage
    {
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
        /// The value
        /// </summary>
        private string value = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePinNotes"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public UpdatePinNotes(string key)
        {
            this.value = key;
            this.InitializeComponent();
            this.UpdatePinData();
        }

        /// <summary>
        /// Updates the pin data.
        /// </summary>
        public async void UpdatePinData()
        {
            try
            {
                FirebaseHelper firebaseHelper = new FirebaseHelper();

                //// Gets current user id
                var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

                ////Gets the notes data
                NotesData notesData = await firebaseHelper.GetNotesData(this.value, userid);

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
                firebaseHelper.UpdateNotes(notes, this.value, userid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return base.OnBackButtonPressed();
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
            firebaseHelper.UnPinnedNotes(notes, this.value, userid);
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
            firebaseHelper.ArchiveNotes(notes, this.value, userid);
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
            firebaseHelper.DeleteNotes(notes, this.value, userid);
        }

        /// <summary>
        /// Handles the Clicked event of the ImageButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            NotesData notes = new NotesData()
            {
                Title = txtTitle.Text,
                Notes = txtNotes.Text,
                ColorNote = this.noteColor
            };
            PopupNavigation.Instance.PushAsync(new PopTaskView(this.value, notes));
        }

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

        /// <summary>
        /// Handles the Clicked event of the TxtBell control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TxtBell_Clicked(object sender, EventArgs e)
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
            };
            PopupNavigation.Instance.PushAsync(new PopUpReminder(this.value, notes));
        }
    }
}
