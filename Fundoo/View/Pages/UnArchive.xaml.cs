// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnArchive.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{
    using System;
    using System.Collections.Generic;
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
        /// The label list
        /// </summary>
        IList<string> labelList = new List<string>();

        string area;

        public bool collabarate = false;

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
            this.labelList = notesData.LabelData;
            this.area = notesData.Area;
            this.BackgroundColor = Color.FromHex(SetColor.GetHexColor(notesData));
            this.collabarate = notesData.IsCollaborated;
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
                ColorNote = this.noteColor,
                LabelData=this.labelList,
                Area=this.area,
                IsCollaborated=this.collabarate,
                Key=this.val
            };
            firebaseHelper.UnArchiveNotes(notes, this.val, userid);
        }
    }
}