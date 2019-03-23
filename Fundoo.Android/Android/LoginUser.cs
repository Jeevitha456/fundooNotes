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
    /// LoginUser
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
                var user1 = FirebaseAuth.Instance.CurrentUser;
                if (user1 != null)
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
                using (var user1 = authResult.User)

                //// Sending the email verification after sign up
                using (var actionCode = ActionCodeSettings.NewBuilder().SetAndroidPackageName(PackageName, true, "0").Build ())
                {
                    await user1.SendEmailVerificationAsync(actionCode);
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
        /// <returns></returns>
        public async Task ResetPass(string email)
        {
            using (var actioncode = ActionCodeSettings.NewBuilder().SetAndroidPackageName(PackageName, true, "0").Build())
            {
                await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);
            }            
        }
        public string  SignOut()
        {
            string status = null;
            try
            {
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

        public bool IsUserLoggedIn()
        {
            if (FirebaseAuth.Instance.CurrentUser!= null)
            {
                return true;
            }
            else
                return false;

        }
    }
}