﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DashBoard.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------

namespace Fundoo.View.Pages
{
    using Fundoo.Database;
    using Fundoo.Firebase;
    using Fundoo.Interface;
    using Fundoo.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    ///  Dash Board Class
    /// </summary>
    public partial class DashBoard : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashBoard"/> class.
        /// </summary>
        public DashBoard()
        {
           this.InitializeComponent();
        }
        NotesDatabase notesDatabase = new NotesDatabase();
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        protected async override void OnAppearing()
        {
           
            var uid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            var notes = await this.notesDatabase.GetNotesAsync(uid);
            if (notes != null)
            {
                this.NoteGridView(notes);
            }
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private  void Button_Clicked(object sender, EventArgs e)
        {
         Navigation.PushAsync(new TakeANote());

        }

       
        public void NoteGridView(IList<NotesData> list)
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



                var productIndex = 0;
                var indexe = -1;

                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < 2; columnIndex++)
                    {
                        NotesData data = null;
                        indexe++;
                        if (indexe < list.Count)
                        {
                            data = list[indexe];
                        }

                        if (productIndex >= list.Count)
                        {
                            break;
                        }

                        productIndex += 1;
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
                          //  Navigation.PushAsync(new UpdateNote(Keyval));
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
    }
}