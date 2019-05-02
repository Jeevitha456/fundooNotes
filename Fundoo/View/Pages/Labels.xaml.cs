// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Labels.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Fundoo.Firebase;
    using Fundoo.Interface;
    using Fundoo.Model;
    using global::Firebase.Database;
    using global::Firebase.Database.Query;
    using Plugin.InputKit.Shared.Controls;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Labels
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Labels : ContentPage
    {
        private FirebaseClient firebase = new FirebaseClient("https://fundooapp-50c31.firebaseio.com/");
        
        /// <summary>
        /// The firebase helper
        /// </summary>
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        string key = null;
        NotesData notes = null;

        NotesData noteModel = new NotesData();
        /// <summary>
        /// Initializes a new instance of the <see cref="Labels"/> class.
        /// </summary>
        public Labels(string value,NotesData notesData)
        {
            this.notes = notesData;
            this.key = value;
            this.InitializeComponent();
         
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected async override void OnAppearing()
        {
            try
            {
                //// Overiding base class on appearing 
                base.OnAppearing();

                //// Listing all the person in the list
                var allLabels = await this.firebaseHelper.GetAllLabels();
               
                lstLabels.ItemsSource = allLabels;
               
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Handles the CheckChanged event of the CheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void CheckBox_CheckChanged(object sender, EventArgs e)
        {

            var checkbox = (CheckBox)sender;
            if (checkbox.IsChecked)
            {            
               checkbox.Color = Color.Black;
                string item = checkbox.Text;
                var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
                NotesData notes =await firebaseHelper.GetNotesData(key,userid);
                notes.LabelData.Add(item);
                 noteModel= new NotesData()
                {
                    Title=notes.Title,
                    Notes=notes.Notes,
                    ColorNote=notes.ColorNote,
                    LabelData=notes.LabelData
                };
                   
                firebaseHelper.AddLabelToNotes(this.key, noteModel);

            }
        }

        /// <summary>
        /// Called when clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="NotImplementedException">OnClicked</exception>
        private void OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}