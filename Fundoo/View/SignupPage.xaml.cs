// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignupPage.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------

namespace Fundoo.View
{
    using System;
    using System.Text.RegularExpressions;
    using Fundoo.Firebase;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// The firebase helper
    /// </summary>
    public partial class SignupPage : ContentPage
    {
        /// <summary>
        /// Firebase Helper
        /// </summary>
        private FirebaseHelper firebase = new FirebaseHelper();

        /// <summary>
        /// Initializes a new instance of the <see cref="SignupPage"/> class.
        /// </summary>
        public SignupPage()
        {
          this.InitializeComponent();
        }

        /// <summary>
        /// Validation  for email
        /// </summary>
        /// <param name="email">returns email</param>
        /// <returns>returns boolean</returns>
        public bool IsValidEmail(string email)
        {
            if (email == "jee")
            {
                return false;
            }
           
            //// Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            throw new NotImplementedException("Please create a test first");
        }

        /// <summary>
        /// Validate class
        /// </summary>
        /// <returns>returns boolean</returns>
        public bool IsValidate()
        {
            //// Checks if user inputs are null
            if (txtFirstName.Text == null || txtFirstName.Text.Contains(" "))
            {
                //// Displays the message
                 this.DisplayAlert("Alert", "Enter First Name", "ok");
                return false; 
            }

            //// Checks if user input is null or not
            if (txtLastName.Text == null || txtLastName.Text.Contains(" "))
            {
                //// Displays the message
                this.DisplayAlert("Alert", "Enter Last Name", "ok");
                return false;
            }

            //// Checks if it is minimum of 3 charachters
            if (txtFirstName.Text.Length < 3)
            {
                 this.DisplayAlert("Alert", "Enter Minimum of 3 characters for  First Name", "ok");
                return false;
            }

            //// Checks if it contains digit
            if (Regex.IsMatch(txtFirstName.Text, @"[0-9]") || Regex.IsMatch(txtLastName.Text, @"[0-9]"))
            {
                 this.DisplayAlert("Alert", "Enter Only characters for Name", "ok");
                return false;
            }

            if (txtEmail.Text == null)
            {
                this.DisplayAlert("Alert", "Enter valid email", "ok");
                return false;
            }
            //// Email validation
            if (!this.IsValidEmail(txtEmail.Text))
            {
                this.DisplayAlert("Alert", "Enter valid email", "ok");
                return false;
            }

            //// Password validation
            if (txtPassword.Text.Length < 6)
            {
                this.DisplayAlert("Alert", "Password should be greater than 6 ", "ok");
                return false;
            }

            //// checks for password
            if (!txtPassword.Text.Equals(txtRepeatPassword.Text))
            {
                this.DisplayAlert("Alert", "Repeat Password is not matching with the password", "ok");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Button sign up
        /// </summary>
        /// <param name="sender">displays name</param>
        /// <param name="e">displays event</param>
        private async void BtnSignup_Clicked(object sender, EventArgs e)
        {
            try
            {
                //// Firebase object
                FirebaseHelper firebase = new FirebaseHelper();
                if (this.IsValidate())
                {
                    //// Callls the AddUser method with user inputs for signup
                    await this.firebase.AddUser(txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPassword.Text, txtRepeatPassword.Text);

                    //// Empty all user input after the data
                    txtFirstName.Text = string.Empty;
                    txtLastName.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    txtRepeatPassword.Text = string.Empty;

                    //// If it is successfull displays mesaage
                    await this.DisplayAlert("Success", "Signed-up successfully", "ok");

                    //// Navigates back to the login view page
                    await Navigation.PushModalAsync(new Login());
                }
                else
                {
                    await this.DisplayAlert("Alert", "Signed-up  not successfully", "ok");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}