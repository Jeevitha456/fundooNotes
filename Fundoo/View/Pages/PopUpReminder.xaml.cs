// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PopUpReminder.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------

namespace Fundoo.View.Pages
{
    using System;
    using Fundoo.Model;
    using Plugin.InputKit.Shared.Controls;
    using Rg.Plugins.Popup.Pages;
    using Rg.Plugins.Popup.Services;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Pop Up Reminder class
    /// </summary>
    /// <seealso cref="Rg.Plugins.Popup.Pages.PopupPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpReminder : PopupPage
    {
        /// <summary>
        /// The random
        /// </summary>
        private static readonly Random rnd = new Random();

        /// <summary>
        /// The value
        /// </summary>
        private string value = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PopUpReminder"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="notesData">The notes data.</param>
        public PopUpReminder(string key)
        {
            this.value = key;
            this.InitializeComponent();
        }

        /// <summary>
        /// Randomizes the colors.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RandomizeColors(object sender, EventArgs e)
        {
            var colors = typeof(Color).GetFields();
            var color = (Color)colors[rnd.Next(colors.Length)].GetValue(null);

            //// Loops over the children elements
            foreach (var view in group.Children)
            {
                if (view is RadioButton rb)
                {
                    rb.Color = color;
                }
            }
        }

        /// <summary>
        /// Handles the Clicked event of the PlaceClicked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PlaceClicked_Clicked(object sender, EventArgs e)
        {         
            Navigation.PushModalAsync(new GeoLocation(this.value));
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}