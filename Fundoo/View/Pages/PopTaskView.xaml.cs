// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PopTaskView.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{ 
    using System;
    using Rg.Plugins.Popup.Pages;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Pop Task View
    /// </summary>
    /// <seealso cref="Rg.Plugins.Popup.Pages.PopupPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopTaskView : PopupPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PopTaskView"/> class.
        /// </summary>
        public PopTaskView()
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
        }
    }
}