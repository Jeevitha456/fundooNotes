// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchNotes.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fundoo.Firebase;
    using Fundoo.Model;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Search Notes Class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchNotes : ContentPage
    {
        /// <summary>
        /// The notes data
        /// </summary>
        private List<NotesData> notesData;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchNotes"/> class.
        /// </summary>
        public SearchNotes()
        {
            this.InitializeComponent();
            this.Data();
            list.ItemsSource = this.notesData;
        }

        /// <summary>
        /// Data this instance.
        /// </summary>
        public async void Data()
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();
            List<NotesData> notes = await firebaseHelper.GetAllNotes();
            notes = notes.Where(a => a.IsDeleted == false && a.IsArchive == false).ToList();
            this.notesData = notes;
        }

        /// <summary>
        /// Handles the TextChanged event of the SearchBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                list.ItemsSource = this.notesData;
            }
            else
            {
                list.ItemsSource = this.notesData.Where(x => x.Title.ToLower().Contains(e.NewTextValue.ToLower())                 
                || x.Notes.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }
    }
}