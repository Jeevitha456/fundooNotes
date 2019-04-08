// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArchievePage.xaml.cs" company="Bridgelabz">
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
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// Page Class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class ArchievePage : ContentPage
    {
        /// <summary>
        /// The notes database
        /// </summary>
        private NotesDatabase notesDatabase = new NotesDatabase();

        /// <summary>
        /// The firebase helper
        /// </summary>
        private FirebaseHelper firebaseHelper = new FirebaseHelper();

        /// <summary>
        /// Initializes a new instance of the <see cref="ArchievePage"/> class.
        /// </summary>
        public ArchievePage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Grids the view.
        /// </summary>
        /// <param name="list">The list.</param>
        public void GridView(IList<NotesData> list)
        {
            try
            {
                GridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });
                GridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });
                GridLayout.Margin = 5;
                int rowCount = 0;
                for (int row = 0; row < list.Count; row++)
                {
                    if (row % 2 == 0)
                    {
                        GridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                        rowCount++;
                    }
                }

                var index = -1;

                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < 2; columnIndex++)
                    {
                        NotesData data = null;

                        index++;
                        if (index < list.Count)
                        {
                            data = list[index];
                        }

                        var label = new Xamarin.Forms.Label
                        {
                            Text = data.Title,
                            TextColor = Color.Black,
                            FontAttributes = FontAttributes.Bold,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Start,
                        };

                        var labelKey = new Xamarin.Forms.Label
                        {
                            Text = data.Key,
                            IsVisible = false
                        };

                        var content = new Xamarin.Forms.Label
                        {
                            Text = data.Notes,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Start,
                        };

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
                            StackLayout layout123 = (StackLayout)sender;
                            IList<View> item = layout123.Children;
                            Xamarin.Forms.Label KeyValue = (Xamarin.Forms.Label)item[0];
                            var Keyval = KeyValue.Text;
                          //  Navigation.PushAsync(new Delete(Keyval));
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
                var uid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
                var notes = await this.notesDatabase.GetNotesAsync();
                if (notes != null)
                {
                    notes = notes.Where(a => a.IsArchive == true).ToList();
                    this.GridView(notes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
