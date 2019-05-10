// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DashBoard.xaml.cs" company="Bridgelabz">
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
    using Rg.Plugins.Popup.Services;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    ///  Dash Board Class
    /// </summary>
    public partial class DashBoard : ContentPage
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
        /// Initializes a new instance of the <see cref="DashBoard"/> class.
        /// </summary>
        public DashBoard()
        {
           this.InitializeComponent();      
        }

        /// <summary>
        /// Grids the view pin.
        /// </summary>
        /// <param name="pinlist">The pin list.</param>
        public void GridViewPin(IList<NotesData> pinlist, IList<CreateNewLabel> listLabel)
        {
            try
            {
                ///// Creates column defination of width 170
                GridLayout1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });
                GridLayout1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });
                GridLayout1.Margin = 5;
                int rowCount = 0;
                //// Creates number of columns as lables are added
                for (int row = 0; row < pinlist.Count; row++)
                {
                    //// Adds new row after 2 labelss
                    if (row % 2 == 0)
                    {
                        GridLayout1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                        rowCount++;
                    }
                }

                var index = -1;

                //// Adds label to row and columns
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < 2; columnIndex++)
                    {
                        NotesData data = null;
                      
                        index++;
                        if (index < pinlist.Count)
                        {
                            data = pinlist[index];
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

                        var colorlabel = new Xamarin.Forms.Label
                        {
                            Text = data.ColorNote,
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
                            ////BackgroundColor = Color.White
                        };

                        var tapGestureRecognizer = new TapGestureRecognizer();
                        layout.Children.Add(labelKey);
                        layout.Children.Add(label);
                        layout.Children.Add(content);
                        layout.Children.Add(colorlabel);
                        layout.GestureRecognizers.Add(tapGestureRecognizer);
                        layout.Spacing = 2;
                        layout.Margin = 2;
                        var frame = new Frame();
                        frame.BorderColor = Color.Black;
                        frame.Content = layout;

                        SetColor setColor = new SetColor();
                        setColor.GetColor(data, frame);
                        //// Loops over the labels class
                        foreach (CreateNewLabel createNewLabel in listLabel)
                        {
                            IList<string> labellist = data.LabelData;

                            //// Loops over the list of labels added to the notes
                            foreach (var labelid in labellist)
                            {
                                //// Checks if the labels are equal from the label list
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
                                    labelFrame.BackgroundColor = Color.FromHex(SetColor.GetHexColor(data));
                                    layout.Children.Add(labelFrame);
                                }
                            }
                        }
                        if (data.Area != null)
                        {
                            var image = new Image
                            {
                                Source = "location.png",
                                VerticalOptions = LayoutOptions.Start,
                                HorizontalOptions = LayoutOptions.Start,
                                HeightRequest = 13,
                                WidthRequest = 13
                            };
                            var location = new Label
                            {
                                Text = data.Area,
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                VerticalOptions = LayoutOptions.StartAndExpand,
                                FontSize = 12,
                            };
                            StackLayout framelayout = new StackLayout()
                            {
                                Spacing = 1,
                                Margin = 1,
                            };
                            framelayout.Children.Add(image);
                            framelayout.Children.Add(location);
                            var locationFrame = new Frame();
                            locationFrame.CornerRadius = 28;
                            locationFrame.HeightRequest = 10;
                            locationFrame.WidthRequest = 30;
                            locationFrame.BorderColor = Color.Gray;
                            locationFrame.Content = framelayout;
                            // locationFrame.Content = image;

                            locationFrame.BackgroundColor = Color.FromHex(SetColor.GetHexColor(data));
                            //layout.Children.Add(image);
                            layout.Children.Add(locationFrame);
                        }

                        tapGestureRecognizer.Tapped += (object sender, EventArgs args) =>
                        {
                            StackLayout stacklayout = (StackLayout)sender;
                            IList<View> item = stacklayout.Children;
                            Xamarin.Forms.Label KeyValue = (Xamarin.Forms.Label)item[0];
                            var Keyval = KeyValue.Text;
                            Navigation.PushAsync(new UpdatePinNotes(Keyval));
                        };
                        GridLayout1.Children.Add(frame, columnIndex, rowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Grids the view.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="listLabel">The list label.</param>
        public void GridView(IList<NotesData> list, IList<CreateNewLabel> listLabel)
        {
            try
            {
                ///// Creates column defination of width 170
                GridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });
                GridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });
                GridLayout.Margin = 5;
                int rowCount = 0;
                //// Creates number of columns as lables are added
                for (int row = 0; row < list.Count; row++)
                {
                    //// Adds new row after 2 labelss
                    if (row % 2 == 0)
                    {
                        GridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                        rowCount++;
                    }
                }

                var index = -1;

                //// Adds label to row and columns
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
                        var colorlabel = new Xamarin.Forms.Label
                        {
                            Text = data.ColorNote,
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
                        };

                        var tapGestureRecognizer = new TapGestureRecognizer();
                        layout.Children.Add(labelKey);
                        layout.Children.Add(label);
                        layout.Children.Add(content);
                        layout.Children.Add(colorlabel);
                        layout.GestureRecognizers.Add(tapGestureRecognizer);
                        layout.Spacing = 2;
                        layout.Margin = 2;
                        var frame = new Frame();
                        frame.BorderColor = Color.Black;
                        frame.Content = layout;

                        //// Initializing color class
                        SetColor setColor = new SetColor();
                        setColor.GetColor(data, frame);

                       //// Loops over the labels class
                        foreach (CreateNewLabel createNewLabel in listLabel)
                        {
                            IList<string> labellist = data.LabelData;

                            //// Loops over the list of labels added to the notes
                            foreach (var labelid in labellist)
                            {
                                //// Checks if the labels are equal from the label list
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
                                    labelFrame.BackgroundColor = Color.FromHex(SetColor.GetHexColor(data));
                                    layout.Children.Add(labelFrame);
                                }
                            }
                        }
                        if(data.Area!=null)
                        {
                            var image = new Image
                            {
                                Source = "location.png",
                                VerticalOptions = LayoutOptions.Start,
                                HorizontalOptions = LayoutOptions.Start,
                                HeightRequest=13,
                                WidthRequest=13
                            };
                            var location = new Label
                            {
                                Text = data.Area,
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                VerticalOptions = LayoutOptions.StartAndExpand,
                                FontSize = 12,
                            };
                            StackLayout framelayout = new StackLayout()
                            {
                                Spacing = 1,
                                Margin = 1,
                            };
                            framelayout.Children.Add(image);
                            framelayout.Children.Add(location);
                            var locationFrame = new Frame();                                                        
                            locationFrame.CornerRadius = 28;
                            locationFrame.HeightRequest = 10;
                            locationFrame.WidthRequest = 30;
                            locationFrame.BorderColor = Color.Gray;
                            locationFrame.Content = framelayout;
                           // locationFrame.Content = image;
                          
                            locationFrame.BackgroundColor = Color.FromHex(SetColor.GetHexColor(data));
                            //layout.Children.Add(image);
                            layout.Children.Add(locationFrame);
                        }
                     
                        //// Tap gesture recognizer
                            tapGestureRecognizer.Tapped += (object sender, EventArgs args) =>
                        {
                            StackLayout stacklayout = (StackLayout)sender;
                            IList<View> item = stacklayout.Children;
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

                //// Gets all labels
                var label = await this.firebaseHelper.GetAllLabels();

                //// Checks if notes is not null
                if (notes != null)
                {
                    var notesPin = notes.Where(a => a.IsDeleted == false && a.IsArchive == false && a.IsPinned == true).ToList();
                    this.GridViewPin(notesPin,label);
                    var notesUnpin = notes.Where(a => a.IsDeleted == false && a.IsArchive == false && a.IsPinned == false).ToList();
                    this.GridView(notesUnpin, label);
                }                             
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button_Clicked(object sender, EventArgs e)
        {
            //// Navigates to take a note page
            Navigation.PushAsync(new TakeANote());
        }

        /// <summary>
        /// Handles the Clicked event of the Search control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Search_Clicked(object sender, EventArgs e)
        { 
            //// Navigates to take a Searchnote page
            Navigation.PushModalAsync(new SearchNotes());
        }

        /// <summary>
        /// Handles the Clicked event of the Grid vertical control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Gridvertical_Clicked(object sender, EventArgs e)
        { 
            //// Navigates to take a grid page
            Navigation.PushAsync(new GridPage());
            this.Navigation.RemovePage(this);
        }

        /// <summary>
        /// Handles the 1 event of the ImageButton_Clicked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            //// PopUp Page
            PopupNavigation.Instance.PushAsync(new PopUpCamera());
        }
    }
}