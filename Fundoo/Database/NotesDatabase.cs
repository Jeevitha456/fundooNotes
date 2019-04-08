// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesDatabase.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Fundoo.Model;
    using Xamarin.Forms;
    using global::Firebase.Database;
    using global::Firebase.Database.Query;
    using Fundoo.Interface;

    /// <summary>
    /// Notes Data base
    /// </summary>
    public class NotesDatabase
    {
        /// <summary>
        /// The firebase
        /// </summary>
        private FirebaseClient firebase = new FirebaseClient("https://fundooapp-50c31.firebaseio.com/");

        /// <summary>
        /// Gets the notes asynchronous.
        /// </summary>
        /// <returns>returns task</returns>
        public async Task<IList<NotesData>> GetNotesAsync()
        {
            var uid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            IList<NotesData> notesData = (await this.firebase.Child("Persons").Child(uid).Child("Notes").OnceAsync<NotesData>()).Select(item => new NotesData
            {
                IsArchive = item.Object.IsArchive,
                IsDeleted = item.Object.IsDeleted,
                Title = item.Object.Title,
                Notes = item.Object.Notes,
                Key = item.Key
            }).ToList();

            return notesData;
        }
    }
}
