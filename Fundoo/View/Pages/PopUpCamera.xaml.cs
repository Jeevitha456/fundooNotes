// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PopUpCamera.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{
    using System;
    using Rg.Plugins.Popup.Pages;
    using Rg.Plugins.Popup.Services;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Pop Up Camera
    /// </summary>
    /// <seealso cref="Rg.Plugins.Popup.Pages.PopupPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpCamera : PopupPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PopUpCamera"/> class.
        /// </summary>
        public PopUpCamera()
        {
          this.InitializeComponent();
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button_Clicked(object sender, EventArgs e)
        {
            //// Navigates to Camera Page
            Navigation.PushModalAsync(new CameraPage());
            PopupNavigation.Instance.PopAsync(true);
        }

        /// <summary>
        /// Handles the 1 event of the Button_Clicked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            //// Navigates to GalleryPage
            Navigation.PushModalAsync(new GalleryPage());
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}