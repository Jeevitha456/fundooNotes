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
    using System.Text;
    using System.Threading.Tasks;
    using global::Firebase.Database;
    using global::Firebase.Database.Query;
    using Fundoo.Model;

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
        /// <param name="uid">The id.</param>
        /// <returns>returns task</returns>
        public async Task<IList<NotesData>> GetNotesAsync(string uid)
        {
            IList<NotesData> notesData = (await this.firebase.Child("Persons").Child(uid).Child("Notes").OnceAsync<NotesData>()).Select(item => new NotesData
            {
                Title = item.Object.Title,
                Notes = item.Object.Notes,
                Key = item.Key
            }).ToList();

            return notesData;
        }
    }
}
