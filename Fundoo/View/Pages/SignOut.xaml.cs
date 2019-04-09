// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignOut.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------

namespace Fundoo.View.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fundoo.Interface;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// SignOut Page Class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class SignOut : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignOut"/> class.
        /// </summary>
        public SignOut()
        {
          this.InitializeComponent();
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnAppearing()
        {
            //// Picks method sign out
            DependencyService.Get<IFirebaseAuthenticator>().SignOut();

            //// Navigates to login page
             Navigation.PushModalAsync(new Login());
        }
    }
}