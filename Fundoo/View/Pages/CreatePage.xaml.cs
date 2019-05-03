// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreatePage.xaml.cs" company="Bridgelabz">
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

    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// Create Page Class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class CreatePage : ContentPage
    {
        /// <summary>
        /// The firebase helper
        /// </summary>
        private FirebaseHelper firebaseHelper = new FirebaseHelper();

        /// <summary>
        /// The create new label
        /// </summary>
        private CreateNewLabel createNewLabel = new CreateNewLabel();

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePage"/> class.
        /// </summary>
        public CreatePage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Grids the view.
        /// </summary>
        /// <param name="list">The list.</param>
        public void GridView(IList<CreateNewLabel> list)
        {
            try
            {
                GridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(350) });
                GridLayout.Margin = 5;
                int rowCount = 0;
                //// Creates number of columns as lables are added
                for (int row = 0; row < list.Count; row++)
                {
                    //// Adds new row after 2 labelss
                    GridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
                    rowCount++;
                }

                var index = -1;

                //// Adds label to row and columns
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < 1; columnIndex++)
                    {
                        CreateNewLabel data = null;

                        index++;
                        if (index < list.Count)
                        {
                            data = list[index];
                        }

                        //// Creates Labels
                        var label = new Xamarin.Forms.Label
                        {
                            Text = data.Label,
                            TextColor = Color.Black,
                            FontAttributes = FontAttributes.None,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Center,                        
                        };

                        //// Created label key
                        var labelKey = new Xamarin.Forms.Label
                        {
                            Text = data.LabelKey,
                            IsVisible = false
                        };

                        //// Image added to layout
                        var image = new Image
                        {
                            Source = "label.png",
                           VerticalOptions = LayoutOptions.Start,
                            HorizontalOptions = LayoutOptions.Start,
                            WidthRequest = 20,
                            HeightRequest = 20,
                        };

                        //// Image added to layout
                        var imageedit = new Image
                        {
                            Source = "editicon.png",
                             VerticalOptions = LayoutOptions.Start,
                            HorizontalOptions = LayoutOptions.End,
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
                        layout.Children.Add(image);
                        layout.Children.Add(imageedit);
                        layout.GestureRecognizers.Add(tapGestureRecognizer);
                        layout.Spacing = 2;
                        layout.Margin = 2;
                        layout.BackgroundColor = Color.White;
                        var frame = new Frame();
                        frame.BorderColor = Color.White;
                        frame.Content = layout;

                        //// Tapped gesture when tapped navigates to Update Labels Page
                        tapGestureRecognizer.Tapped += (object sender, EventArgs args) =>
                        {
                            StackLayout stacklayout = (StackLayout)sender;
                            IList<Xamarin.Forms.View> item = stacklayout.Children;
                            Xamarin.Forms.Label KeyValue = (Xamarin.Forms.Label)item[0];
                            var Keyval = KeyValue.Text;
                            Navigation.PushAsync(new UpdateLabels(Keyval));
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

                //// Gets all the notes
                var notes = await this.firebaseHelper.GetAllLabels();

                //// Checks if the note is not null
                if (notes != null)
                {
                    this.GridView(notes);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Handles the Clicked event of the ImageButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                //// Creats labels in firebase
                await this.firebaseHelper.CreateLabel(txtLabel.Text);

                //// Empty the placeholder
                txtLabel.Text = string.Empty;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}