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
    using global::Firebase.Database;
    using global::Firebase.Database.Query;
    using Fundoo.Interface;
    using Fundoo.Model;
    using Xamarin.Forms;

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
            //// Gets the current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            try
            {
                ////// Returns all the data
                IList<NotesData> notesData = (await this.firebase.Child("Persons").Child(userid).Child("Notes").OnceAsync<NotesData>()).Select(item => new NotesData
                {
                    IsPinned = item.Object.IsPinned,
                    IsArchive = item.Object.IsArchive,
                    IsDeleted = item.Object.IsDeleted,
                    IsCollaborated=item.Object.IsCollaborated,
                    Title = item.Object.Title,
                    Notes = item.Object.Notes,
                    Key = item.Key,
                    ColorNote = item.Object.ColorNote,
                    LabelData = item.Object.LabelData,
                    Longitude = item.Object.Longitude,
                    Latitude = item.Object.Latitude,
                    Area = item.Object.Area
                }).ToList();
                return notesData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
