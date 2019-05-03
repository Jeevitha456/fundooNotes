// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Labels.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{
    using System;
    using global::Firebase.Database;
    using Fundoo.Firebase;
    using Fundoo.Interface;
    using Fundoo.Model;
    using Plugin.InputKit.Shared.Controls;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Labels class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Labels : ContentPage
    {
        /// <summary>
        /// The firebase
        /// </summary>
        private FirebaseClient firebase = new FirebaseClient("https://fundooapp-50c31.firebaseio.com/");
        
        /// <summary>
        /// The firebase helper
        /// </summary>
        private FirebaseHelper firebaseHelper = new FirebaseHelper();

        /// <summary>
        /// The key
        /// </summary>
        private string key = null;

        /// <summary>
        /// The notes
        /// </summary>
        private NotesData notes = null;

        /// <summary>
        /// The note model
        /// </summary>
        private NotesData noteModel = new NotesData();

        /// <summary>
        /// Initializes a new instance of the <see cref="Labels"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="notesData">The notes data.</param>
        public Labels(string value, NotesData notesData)
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

            //// Checks if the checkbox is checked
            if (checkbox.IsChecked)
            {            
               //// Assigns color to the checkbox when ticked
               checkbox.Color = Color.Black;

                //// Gets the description of the checkbox when checked
                string item = checkbox.Text;

                //// Gets the currents user id
                var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

                //// Gets the notes data choosen
                NotesData notes = await this.firebaseHelper.GetNotesData(this.key, userid);
                notes.LabelData.Add(item);
                this.noteModel = new NotesData()
                {
                    Title = notes.Title,
                    Notes = notes.Notes,
                    ColorNote = notes.ColorNote,
                    LabelData = notes.LabelData
                };
               
               //// Updates it to the firebase
               this.firebaseHelper.AddLabelToNotes(this.key, this.noteModel);
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