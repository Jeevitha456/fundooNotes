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

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TakeANote());
        }
    }
}