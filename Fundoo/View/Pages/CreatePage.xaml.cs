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
        CreateNewLabel createNewLabel = new CreateNewLabel();
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePage"/> class.
        /// </summary>
        public CreatePage()
        {
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
                var allLabels = await this.firebaseHelper.GetAllLabels();
                lstLabels.ItemsSource = allLabels;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
        
            Label label  = (Label)sender;
            var labelKey = label.Text;
            
            await Navigation.PushModalAsync(new UpdateLabels(labelKey));
        }
    }
}