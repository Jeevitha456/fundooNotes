using Firebase.Database;
using Firebase.Database.Query;
using Fundoo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundoo.Database
{
    public class NotesDatabase
    {
        private FirebaseClient firebase = new FirebaseClient("https://fundooapp-50c31.firebaseio.com/");
        public async Task<IList<NotesData>> GetNotesAsync(string uid)
        {
            IList<NotesData> notesData = (await this.firebase.Child("Persons").Child(uid).Child("userinfo").OnceAsync<NotesData>()).Select(item => new NotesData
            {
                Title = item.Object.Title,
                Notes = item.Object.Notes,
                Key = item.Key
            }).ToList();

            return notesData;
        }

    }
}
