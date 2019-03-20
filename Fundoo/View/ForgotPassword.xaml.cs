// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ForgotPassword.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View
{
    using System;
    using Fundoo.Firebase;
    using Fundoo.Interface;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// The Forgot Password
    /// </summary>
    public partial class ForgotPassword : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPassword"/> class.
        /// </summary>
        public ForgotPassword()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the Clicked event of the Button Submit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void BtnSubmit_Clicked(object sender, EventArgs e)
        {     
            await DependencyService.Get<IFirebaseAuthenticator>().ResetPass(txtEmail.Text);
            await Navigation.PushModalAsync(new Login());
        }
    }
}