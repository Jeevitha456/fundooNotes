// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Login.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View
{
    using System;
    using System.Collections.Generic;
    using Fundoo.Firebase;
    using Fundoo.Interface;
    using Fundoo.View.HomePage;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// Login class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class Login : ContentPage
    {
        /// <summary>
        /// The firebase helper
        /// </summary>
        private FirebaseHelper firebaseHelper = new FirebaseHelper();

        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        public Login()
        {
            this.InitializeComponent();
        }
        private List<string> listEmail = new List<string>();
     //   private List<string> myList = new List<string>();
     
        /// <summary>
        /// Handles the Clicked event of the Button Login control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                //// Using dependency service and logging in with email and password
                var validate = await DependencyService.Get<IFirebaseAuthenticator>().LoginWithEmailPassword(txtEmail.Text, txtPassword.Text);
                var uid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
                this.listEmail.Add(txtEmail.Text);
                if (listEmail.Contains(uid))
                {
                    listEmail.Remove(uid);
                }
               
                //// checks if its a valid email and password
                if (validate)
                {
                    //// Navigates to the homepage
                    await Navigation.PushModalAsync(new Master());
                }
                else
                {
                    //// displays an alert
                    await this.DisplayAlert("Hello", "Invalid email or password", "ok");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }           
        }

        public List<string> GetList()
        {
            return listEmail;
        }

        /// <summary>
        /// Handles the Clicked event of the Button Signup control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnSignup_Clicked(object sender, EventArgs e)
        {
            try
            {
                //// Navigates to signup page
                Navigation.PushModalAsync(new SignupPage());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            //this.listEmail.Add(txtEmail.Text);
        }

        /// <summary>
        /// Handles the Clicked event of the Button ForgotPassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnForgotPassword_Clicked(object sender, EventArgs e)
        {
            try
            {
                //// Navigates to forgot password
                Navigation.PushModalAsync(new ForgotPassword());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
} 