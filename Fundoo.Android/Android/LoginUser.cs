// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginUser.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------

using Fundoo.Droid.Android;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoginUser))]

namespace Fundoo.Droid.Android
{
    using System;
    using System.Threading.Tasks;
    using global::Firebase.Auth;
    using Fundoo.Interface;

    /// <summary>
    /// Login User
    /// </summary>
    /// <seealso cref="Fundoo.Interface.IFirebaseAuthenticator" />
    public class LoginUser : IFirebaseAuthenticator
    {
        /// <summary>
        /// Gets the name of the package.
        /// </summary>
        /// <value>
        /// The name of the package.
        /// </value>
        public static string PackageName { get; }

        /// <summary>
        /// Logins the with email password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// returns boolean
        /// </returns>
        public async Task<bool> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                //// Authenticates with firebase for sign up with email and password
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);

                //// gets the satatus
                var token = this.Status();
                return token;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Statuses this instance.
        /// </summary>
        /// <returns>returns boolean</returns>
        public bool Status()
        {
            try
            {
                //// Gets the current user from the firebase
                var user = FirebaseAuth.Instance.CurrentUser;
                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Signs up with email password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// returns string
        /// </returns>
        public async Task<string> SignUpWithEmailPassword(string email, string password)
        {
            try
            {
                //// Allows the user to create and sign up with user inputs using firebase
                 var authResult = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                using (var user = authResult.User)

                //// Sending the email verification after sign up
                using (var actionCode = ActionCodeSettings.NewBuilder().SetAndroidPackageName(PackageName, true, "0").Build())
                {
                    await user.SendEmailVerificationAsync(actionCode);
                }

                return authResult.User.Uid;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Resets the pass.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>returns task</returns>
        public async Task ResetPass(string email)
        {
            using (var actioncode = ActionCodeSettings.NewBuilder().SetAndroidPackageName(PackageName, true, "0").Build())
            {
                await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);
            }            
        }

        /// <summary>
        /// Represents an event that is raised when the sign-out operation is complete.
        /// </summary>
        /// <returns>
        /// returns string
        /// </returns>
        public string SignOut()
        {
            string status = null;
            try
            {
                //// Sign out the current user
                FirebaseAuth.Instance.SignOut();
                status = FirebaseAuth.Instance.CurrentUser.Uid;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return status;
        }

        /// <summary>
        /// Determines whether [is user logged in].
        /// </summary>
        /// <returns>
        /// <c>true</c> if [is user logged in]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUserLoggedIn()
        {
            //// Checks if user is logged in or not
            if (FirebaseAuth.Instance.CurrentUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }             
        }

        /// <summary>
        /// Users the identifier.
        /// </summary>
        /// <returns>
        /// returns string
        /// </returns>
        public string UserId()
        {
            string uid = null;
            try
            {
                uid = FirebaseAuth.Instance.CurrentUser.Uid;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return uid;
        }
    }
}