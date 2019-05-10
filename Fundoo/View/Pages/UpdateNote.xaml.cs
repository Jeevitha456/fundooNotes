// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateNote.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{
    using System;
    using System.Collections.Generic;
    using Fundoo.Database;
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
        /// <summary>
        /// The note color
        /// </summary>
        private string noteColor = "White";

        /// <summary>
        /// Lists of string
        /// </summary>
        IList<string> lists = new List<string>();

        string area;

        /// <summary
        /// >
        /// The value
        /// </summary>
        private string value = null;

        /// <summary>
        /// The notes database
        /// </summary>
        private NotesDatabase notesDatabase = new NotesDatabase();

        /// <summary>
        /// The firebase helper
        /// </summary>
        private FirebaseHelper firebaseHelper = new FirebaseHelper();

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNote"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public UpdateNote(string key)
        {
            this.value = key;
            this.InitializeComponent();
            this.UpdateData();
        }

        /// <summary>
        /// Gets or sets the color notes.
        /// </summary>
        /// <value>
        /// The color notes.
        /// </value>
        public Color ColorNotes { get; set; }

        /// <summary>
        /// Labels the list.
        /// </summary>
        /// <param name="label">The label.</param>
        public async void LabelList(IList<string> label)
        {        
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            var allLabels = await this.firebaseHelper.GetAllLabels();
            foreach (CreateNewLabel createNewLabel in allLabels)
            {
                foreach (var labelid in label)
                {
                    if (createNewLabel.LabelKey.Equals(labelid))
                    {
                        var labelName = new Label
                        {
                            Text = createNewLabel.Label,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Start,
                            FontSize = 12,
                        };
                        var labelFrame = new Frame();
                        labelFrame.CornerRadius = 28;
                        labelFrame.HeightRequest = 14;
                        labelFrame.BorderColor = Color.Gray;
                        labelFrame.Content = labelName;
                        labelFrame.BackgroundColor = this.BackgroundColor;
                        labelLayout.Children.Add(labelFrame);
                    }
                }
            }
        }

        public async void LocationArea(string Area)
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            var notes= await this.firebaseHelper.GetNotesData(value,userid);
            if(notes.Area!=null)
            {
                var image = new Image
                {
                    Source = "location.png",
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start,
                   
                    HeightRequest = 13,
                    WidthRequest = 13
                };
                var location = new Label
                {
                    Text = notes.Area,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start,
                    FontSize = 14,
                   
                };
                StackLayout framelayout = new StackLayout()
                {
                  Spacing=2,
                  Margin=1

                };
                var tapGestureRecognizer = new TapGestureRecognizer();
                framelayout.GestureRecognizers.Add(tapGestureRecognizer);
                framelayout.Children.Add(image);
                framelayout.Children.Add(location);
                var locationFrame = new Frame();
                locationFrame.CornerRadius = 28;
                locationFrame.HeightRequest = 14;
                location.WidthRequest = 50;
                locationFrame.BorderColor = Color.Gray;
                locationFrame.Content = framelayout;
                // locationFrame.Content = image;

                locationFrame.BackgroundColor = Color.FromHex(SetColor.GetHexColor(notes));

                tapGestureRecognizer.Tapped += (object sender, EventArgs args) =>
                {
                    StackLayout stacklayout = (StackLayout)sender;
                    IList<View> item = stacklayout.Children;
                  
                    Navigation.PushAsync(new GeoLocation());
                };
                //layout.Children.Add(image);
                locationLayout.Children.Add(locationFrame);
            }
           

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
                NotesData notesData = await firebaseHelper.GetNotesData(this.value, userid);

                txtTitle.Text = notesData.Title;
                txtNotes.Text = notesData.Notes;
                this.noteColor = notesData.ColorNote;
                this.lists=notesData.LabelData;
                this.area = notesData.Area;
                this.BackgroundColor = Color.FromHex(SetColor.GetHexColor(notesData));
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
                    ColorNote = this.noteColor,
                    LabelData=this.lists,
                    Area=this.area,
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
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected async override void OnAppearing()
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            var notesData = await this.firebaseHelper.GetNotesData(this.value, userid);
            var label = notesData.LabelData;
            string location = notesData.Area;
            this.LabelList(label);
            LocationArea(location);
            base.OnAppearing();
        }

        /// <summary>
        /// Handles the Clicked event of the ImageButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            //// Gets current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            //// Updates the notes whenUpdateNotes method is called
            NotesData notes = new NotesData()
            {
                Title = txtTitle.Text,
                Notes = txtNotes.Text,
                ColorNote = this.noteColor,
                LabelData=this.lists,
                Area=this.area
            };
            PopupNavigation.Instance.PushAsync(new PopTaskView(this.value, notes));
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
                IsDeleted = true,
                LabelData=this.lists,
                Area=this.area
            };
            firebaseHelper.DeleteNotes(notes, this.value, userid);
            Navigation.RemovePage(this);
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
                ColorNote = this.noteColor,
                LabelData=this.lists,
                Area=this.area
            };
            firebaseHelper.ArchiveNotes(notes, this.value, userid);
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
                ColorNote = this.noteColor,
                LabelData=this.lists,
                Area=this.area
            };
            firebaseHelper.PinnedNotes(notes, this.value, userid);
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
        /// Handles the 1 event of the ImageButton_Clicked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopPlus());
        }

        /// <summary>
        /// Handles the Clicked event of the TxtBell control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void TxtBell_Clicked(object sender, EventArgs e)
        { 
          await  PopupNavigation.Instance.PushAsync(new PopUpReminder(this.value));
        }
    }
}