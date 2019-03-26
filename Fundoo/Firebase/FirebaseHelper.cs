// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FirebaseHelper.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.Firebase
{
    using System;
    using System.Threading.Tasks;
    using global::Firebase.Database;
    using global::Firebase.Database.Query;
    using Fundoo.Interface;
    using Fundoo.Model;
    using Xamarin.Forms;
  
    /// <summary>
    /// FireBase Class
    /// </summary>
    public class FirebaseHelper
    {
        /// <summary>
        /// The firebase
        /// </summary>
        public FirebaseClient firebase = new FirebaseClient("https://fundooapp-50c31.firebaseio.com/");
 
        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="repeatPassword">The repeat password.</param>
        /// <returns>returns task</returns>
        public async Task AddUser(string firstName, string lastName, string email, string password, string repeatPassword)
        {          
            try
            {      
                //// used for signing up with email and password
                var userid = await DependencyService.Get<IFirebaseAuthenticator>().SignUpWithEmailPassword(email, password);

                //// Creats persons object to add to the firebase
                await this.firebase.Child("Persons").Child(userid).Child("userinfo").PostAsync(new SignUpUserData() { FirstName = firstName, LastName = lastName, Email = email });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }                                     
        }

        /// <summary>
        /// Adds the note.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="note">The note.</param>
        /// <returns>returns task</returns>
        public async Task AddNote(string title, string note)
        {
            try
            {
                //// Getting the user id
                var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

                //// Adding notes given id
                await this.firebase.Child("Persons").Child(userid).Child("userinfo").PostAsync(new NotesData() { Title = title, Notes = note });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }       
    }
}
