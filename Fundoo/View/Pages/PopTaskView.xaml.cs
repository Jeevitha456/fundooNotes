// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PopTaskView.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{ 
    using System;
    using Fundoo.Firebase;
    using Fundoo.Interface;
    using Fundoo.Model;
    using Rg.Plugins.Popup.Pages;
    using Rg.Plugins.Popup.Services;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Pop Task View
    /// </summary>
    /// <seealso cref="Rg.Plugins.Popup.Pages.PopupPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopTaskView : PopupPage
    {
        /// <summary>
        /// The value
        /// </summary>
        private string value = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PopTaskView"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public PopTaskView(string key)
        {
            this.value = key;
          this.InitializeComponent();
        }

        /// <summary>
        /// Handles the 1 event of the Button_Clicked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Labels());
            PopupNavigation.Instance.PopAsync(true);
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            NotesData notesData = await firebaseHelper.GetNotesData(this.value, userid);
            await Share.RequestAsync(new ShareTextRequest
            {              
               Text = notesData.Notes,
               Title = "Share Text"
            });
          await PopupNavigation.Instance.PopAsync(true);
        }

        /// <summary>
        /// Reds the button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RedButton(object sender, EventArgs e)
        {
        }
    }
}