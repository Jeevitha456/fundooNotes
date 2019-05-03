// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Delete.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Fundoo.Firebase;
    using Fundoo.Interface;
    using Fundoo.Model;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Delete Deleting class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Delete : ContentPage
    {
        /// <summary>
        /// The value
        /// </summary>
        private string val = null;

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
        /// Initializes a new instance of the <see cref="Delete"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Delete(string value)
        {
            this.val = value;
            this.InitializeComponent();
            this.DeleteData();
        }

        /// <summary>
        /// Deletes the data.
        /// </summary>
        public async void DeleteData()
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Handles the Clicked event of the Delete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Delete_Clicked(object sender, EventArgs e)
        {
            try
            {
                FirebaseHelper firebaseHelper = new FirebaseHelper();
                //// Gets the current user id
                var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
                //// Delets the data from trash and from database
                NotesData notes = new NotesData()
                {
                    Title = txtTitle.Text,
                    Notes = txtNotes.Text,
                    ColorNote = this.noteColor
                };
                firebaseHelper.DeleteForever(notes, this.val, userid);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        /// <summary>
        /// Handles the Clicked event of the Restore control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Restore1_Clicked(object sender, EventArgs e)
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            //// Gets current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            //// Updates the notes when ArchiveNotes method is called
            NotesData notes = new NotesData()
            {
                Title = txtTitle.Text,
                Notes = txtNotes.Text,
                IsDeleted = false,
                ColorNote = this.noteColor
            };
            firebaseHelper.RestoreNotes(notes, this.val, userid);
        }
    }
}