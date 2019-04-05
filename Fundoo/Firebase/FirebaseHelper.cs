﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FirebaseHelper.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.Firebase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        private FirebaseClient firebase = new FirebaseClient("https://fundooapp-50c31.firebaseio.com/");
 
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
        /// Adds the note
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="note">The note.</param>
        public void AddNote(string title, string note)
        {
            try
            {
                //// Getting the user id
                var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

                //// Adding notes given id
                 this.firebase.Child("Persons").Child(userid).Child("Notes").PostAsync(new NotesData() { Title = title, Notes = note });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>returns task</returns>
        public async Task CreateLabel(string label)
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            await this.firebase.Child("Persons").Child(userid).Child("Label").PostAsync(new CreateNewLabel { Label = label });
        }

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <returns>returns task</returns>
        public async Task<List<CreateNewLabel>> GetAllLabels()
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            //// returns all the person contained in the list
            return (await this.firebase
              .Child("Persons").Child(userid).Child("Label").OnceAsync<CreateNewLabel>()).Select(item => new CreateNewLabel
              {
                  Label = item.Object.Label                 
              }).ToList();
        }

        public async Task<List<NotesData>> GetAllNotes()
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            return (await this.firebase
              .Child("Persons").Child(userid).Child("Notes").OnceAsync<NotesData>()).Select(item => new NotesData
              {
                  Title=item.Object.Title,
                  Notes=item.Object.Notes,
                  Key=item.Key
              }).ToList();
        }
        public async Task<NotesData> GetNotesData(string key,string uid)
        {
            NotesData notes = await firebase.Child("Persons").Child(uid).Child("Notes").Child(key).OnceSingleAsync<NotesData>();
           return notes;
        }
        public void UpdateNotes(NotesData notes,string key,string uid)
        {
            try
            {
                this.firebase.Child("Persons").Child(uid).Child("Notes").Child(key).PutAsync(new NotesData() { Title = notes.Title, Notes = notes.Notes });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void DeleteForever(NotesData notes, string key, string uid)
        {
            firebase.Child("Persons").Child(uid).Child("Notes").Child(key).DeleteAsync();
        }
        public void DeleteNotes(NotesData notes, string key, string uid)
        {
           
            this.firebase.Child("Persons").Child(uid).Child("Notes").Child(key).PutAsync(new NotesData() { Title = notes.Title, Notes = notes.Notes,IsDeleted=true});
        }
        public void ArchiveNotes(NotesData notes, string key, string uid)
        {

            this.firebase.Child("Persons").Child(uid).Child("Notes").Child(key).PutAsync(new NotesData() { Title = notes.Title, Notes = notes.Notes, IsArchive = true });
        }
    }
}
