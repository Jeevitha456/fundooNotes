// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreatePage.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------

namespace Fundoo.View.Pages
{
    using Fundoo.Firebase;
    using Fundoo.Model;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    ///  Create Page Class
    /// </summary>
    public partial class CreatePage : ContentPage
    {
      
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePage"/> class.
        /// </summary>
        public CreatePage()
        {
            this.InitializeComponent();         
            
        }
       
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        protected async override void OnAppearing()
        {
            try
            {
                //// Overiding base class on appearing 
                base.OnAppearing();

                //// Listing all the person in the list
                var allLabels = await firebaseHelper.GetAllLabels();
                lstLabels.ItemsSource = allLabels;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await firebaseHelper.CreateLabel(txtLabel.Text);

                //// Empty the placeholder
                txtLabel.Text = string.Empty;
                var allLabels = await firebaseHelper.GetAllLabels();
                lstLabels.ItemsSource = allLabels;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }
    }
}