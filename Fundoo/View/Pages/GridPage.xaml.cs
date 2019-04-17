// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridPage.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fundoo.Database;
    using Fundoo.Firebase;
    using Fundoo.Interface;
    using Fundoo.Model;
    using Fundoo.View.HomePage;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Grid Page Class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridPage : ContentPage
    {
        /// <summary>
        /// notes Database
        /// </summary>
        private NotesDatabase notesDatabase = new NotesDatabase();

        /// <summary>
        /// The firebase helper
        /// </summary>
        private FirebaseHelper firebaseHelper = new FirebaseHelper();

        /// <summary>
        /// Initializes a new instance of the <see cref="GridPage"/> class.
        /// </summary>
        public GridPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Grid View 
        /// </summary>
        /// <param name="list">list of notes.</param>
        public void GridView(IList<NotesData> list)
        {
            try
            {
                ///// Creates column defination of width 170

                GridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(340) });
                GridLayout.Margin = 5;
                int rowCount = 0;
                //// Creates number of columns as lables are added
                for (int row = 0; row < list.Count; row++)
                {
                    //// Adds new row after 2 labelss
                    GridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                    rowCount++;
                }

                var index = -1;

                //// Adds label to row and columns
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < 1; columnIndex++)
                    {
                        NotesData data = null;

                        index++;
                        if (index < list.Count)
                        {
                            data = list[index];
                        }

                        //// Creates Labels
                        var label = new Xamarin.Forms.Label
                        {
                            Text = data.Title,
                            TextColor = Color.Black,
                            FontAttributes = FontAttributes.Bold,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Start,
                        };

                        //// Created label key
                        var labelKey = new Xamarin.Forms.Label
                        {
                            Text = data.Key,
                            IsVisible = false
                        };

                        //// Content view
                        var content = new Xamarin.Forms.Label
                        {
                            Text = data.Notes,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Start,
                        };

                        //// Creates stack layout for each label
                        StackLayout layout = new StackLayout()
                        {
                            Spacing = 2,
                            Margin = 2,
                            BackgroundColor = Color.White
                        };

                        var tapGestureRecognizer = new TapGestureRecognizer();
                        layout.Children.Add(labelKey);
                        layout.Children.Add(label);
                        layout.Children.Add(content);
                        layout.GestureRecognizers.Add(tapGestureRecognizer);
                        layout.Spacing = 2;
                        layout.Margin = 2;
                        layout.BackgroundColor = Color.White;
                        var frame = new Frame();
                        frame.BorderColor = Color.Black;
                        frame.Content = layout;
                        tapGestureRecognizer.Tapped += (object sender, EventArgs args) =>
                        {
                            StackLayout stacklayout = (StackLayout)sender;
                            IList<Xamarin.Forms.View> item = stacklayout.Children;
                            Xamarin.Forms.Label KeyValue = (Xamarin.Forms.Label)item[0];
                            var Keyval = KeyValue.Text;
                            Navigation.PushAsync(new UpdateNote(Keyval));
                        };
                        GridLayout.Children.Add(frame, columnIndex, rowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
                //// Gets current user id
                var uid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

                //// Gets all the notes
                var notes = await this.notesDatabase.GetNotesAsync();

                if (notes != null)
                {
                    //// Displays notes on dashboard
                    notes = notes.Where(a => a.IsDeleted == false && a.IsArchive == false).ToList();
                    this.GridView(notes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Grid vertical
        /// </summary>
        /// <param name="sender">name.</param>
        /// <param name="e">event name</param>
        private void Gridvertical_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Master());
        }
    }
}