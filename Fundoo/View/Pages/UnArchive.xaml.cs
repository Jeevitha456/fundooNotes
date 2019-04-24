// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnArchive.xaml.cs" company="Bridgelabz">
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
    /// UnArchive Class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnArchive : ContentPage
    {
        /// <summary>
        /// The value
        /// </summary>
        private string val = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnArchive"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public UnArchive(string value)
        {
            this.val = value;
            this.UnArchiveData();
            this.InitializeComponent();
        }
        public Color ColorNotes { get; set; }
        private string noteColor = "White";
        /// <summary>
        /// archive data.
        /// </summary>
        public async void UnArchiveData()
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            //// Gets the current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            //// Gets all notes data from firebase
            NotesData notesData = await firebaseHelper.GetNotesData(this.val, userid);

            txtTitle.Text = notesData.Title;
            txtNotes.Text = notesData.Notes;
            this.noteColor = notesData.ColorNote;
        }

        /// <summary>
        /// Handles the Clicked event of the UnArchive1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void UnArchive1_Clicked(object sender, EventArgs e)
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            //// Gets current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            //// Unarchives the notes and removes the notes from the list
            NotesData notes = new NotesData()
            {
                Title = txtTitle.Text,
                Notes = txtNotes.Text,
                IsArchive = false,
                ColorNote = this.noteColor
            };
            firebaseHelper.UnArchiveNotes(notes, this.val, userid);
        }
    }
}