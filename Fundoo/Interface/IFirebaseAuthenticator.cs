// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFirebaseAuthenticator.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.Interface
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface used in login user
    /// </summary>
    public interface IFirebaseAuthenticator
    {
        /// <summary>
        /// Logins the with email password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>returns boolean</returns>
        Task<bool> LoginWithEmailPassword(string email, string password);

        /// <summary>
        /// Signs up with email password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>returns string </returns>
        Task<string> SignUpWithEmailPassword(string email, string password);

        /// <summary>
        /// Resets the pass.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>returns boolean</returns>
        Task ResetPass(string email);

        string SignOut();

        bool IsUserLoggedIn();
        string UserId();
    }
}