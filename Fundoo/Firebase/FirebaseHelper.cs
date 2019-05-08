// --------------------------------------------------------------------------------------------------------------------
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
        /// The note color
        /// </summary>
        private string noteColor = "White";

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
        /// Adds the note.
        /// </summary>
        /// <param name="notes">The notes.</param>
        public void AddNote(NotesData notes)
        {
            try
            {
                //// Getting the user id
                var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

                //// Adding notes given id
                this.firebase.Child("Persons").Child(userid).Child("Notes").PostAsync(new NotesData() { Title = notes.Title, Notes = notes.Notes, ColorNote = this.noteColor, LabelData = notes.LabelData });
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
            try
            {
                //// Gets the current user id
                var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

                //// creates the label
                await this.firebase.Child("Persons").Child(userid).Child("Label").PostAsync(new CreateNewLabel { Label = label });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <returns>returns task</returns>
        public async Task<List<CreateNewLabel>> GetAllLabels()
        {
            //// Gets the current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            //// returns all the person contained in the list
            return (await this.firebase
              .Child("Persons").Child(userid).Child("Label").OnceAsync<CreateNewLabel>()).Select(item => new CreateNewLabel
              {
                  Label = item.Object.Label,
                  LabelKey = item.Key
              }).ToList();
        }

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <returns>returns Task</returns>
        public async Task<List<NotesData>> GetAllNotes()
        {
            //// Gets the current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            //// Returns all the data from the firebase
            return (await this.firebase
              .Child("Persons").Child(userid).Child("Notes").OnceAsync<NotesData>()).Select(item => new NotesData
              {
                  IsArchive = item.Object.IsArchive,
                  IsDeleted = item.Object.IsDeleted,
                  IsPinned = item.Object.IsPinned,
                  LabelData = item.Object.LabelData,
                  Title = item.Object.Title,
                  Notes = item.Object.Notes,
                  Key = item.Key,
                  ColorNote = item.Object.ColorNote
              }).ToList();
        }

        /// <summary>
        /// Gets the notes data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="uid">The id.</param>
        /// <returns>returns Task</returns>
        public async Task<NotesData> GetNotesData(string key, string uid)
        {
            //// Returns the notes from the firebase
            NotesData notes = await this.firebase.Child("Persons").Child(uid).Child("Notes").Child(key).OnceSingleAsync<NotesData>();
            return notes;
        }

        /// <summary>
        /// Adds the label to notes.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="notes">The notes.</param>
        public void AddLabelToNotes(string key, NotesData notes)
        {
            try
            {
                //// Gets the current user id
                var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

                //// Adds the label to the notes choosen
                this.firebase.Child("Persons").Child(userid).Child("Notes").Child(key).PutAsync(new NotesData() { Title = notes.Title, Notes = notes.Notes, ColorNote = notes.ColorNote, LabelData = notes.LabelData });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Gets the labels data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="uid">The id.</param>
        /// <returns>returns label</returns>
        public async Task<CreateNewLabel> GetLabelsData(string key, string uid)
        {
            //// Returns the label from notes choosen
            CreateNewLabel label = await this.firebase.Child("Persons").Child(uid).Child("Label").Child(key).OnceSingleAsync<CreateNewLabel>();
            return label;
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notes">The notes.</param>
        /// <param name="key">The key.</param>
        /// <param name="uid">The id.</param>
        public void UpdateNotes(NotesData notes, string key, string uid)
        {
            try
            {
                //// Updates yhe notes in the firebase
                this.firebase.Child("Persons").Child(uid).Child("Notes").Child(key).PutAsync(new NotesData() { Title = notes.Title, Notes = notes.Notes, ColorNote = notes.ColorNote });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Updates the labels.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="key">The key.</param>
        /// <param name="uid">The id.</param>
        public void UpdateLabels(CreateNewLabel label, string key, string uid)
        {
            try
            {
                //// Updates the label in firebase
                this.firebase.Child("Persons").Child(uid).Child("Label").Child(key).PutAsync(new CreateNewLabel() { Label = label.Label });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the forever.
        /// </summary>
        /// <param name="notes">The notes.</param>
        /// <param name="key">The key.</param>
        /// <param name="uid">The id.</param>
        public void DeleteForever(NotesData notes, string key, string uid)
        {
            try
            {
                //// Deletes the notes from the firebase
                this.firebase.Child("Persons").Child(uid).Child("Notes").Child(key).DeleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="notes">The notes.</param>
        /// <param name="key">The key.</param>
        /// <param name="uid">The id.</param>
        public void DeleteNotes(NotesData notes, string key, string uid)
        {
            try
            {
                //// Deletes the notes from the dashboard
                this.firebase.Child("Persons").Child(uid).Child("Notes").Child(key).PutAsync(new NotesData() { Title = notes.Title, Notes = notes.Notes, IsDeleted = true, ColorNote = notes.ColorNote });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="key">The key.</param>
        /// <param name="uid">The id.</param>
        public void DeleteLabel(CreateNewLabel label, string key, string uid)
        {
            try
            {
                //// Deletes the choosen label
                this.firebase.Child("Persons").Child(uid).Child("Label").Child(key).DeleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Archives the notes.
        /// </summary>
        /// <param name="notes">The notes.</param>
        /// <param name="key">The key.</param>
        /// <param name="uid">The id.</param>
        public void ArchiveNotes(NotesData notes, string key, string uid)
        {
            try
            {
                //// Archives the notes 
                this.firebase.Child("Persons").Child(uid).Child("Notes").Child(key).PutAsync(new NotesData() { Title = notes.Title, Notes = notes.Notes, IsArchive = true, ColorNote = notes.ColorNote });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        ///  Unarchive notes.
        /// </summary>
        /// <param name="notes">The notes.</param>
        /// <param name="key">The key.</param>
        /// <param name="uid">The id.</param>
        public void UnArchiveNotes(NotesData notes, string key, string uid)
        {
            try
            {
                //// UnArchives the notes 
                this.firebase.Child("Persons").Child(uid).Child("Notes").Child(key).PutAsync(new NotesData() { Title = notes.Title, Notes = notes.Notes, IsArchive = false, ColorNote = notes.ColorNote });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Restores the notes.
        /// </summary>
        /// <param name="notes">The notes.</param>
        /// <param name="key">The key.</param>
        /// <param name="uid">The id.</param>
        public void RestoreNotes(NotesData notes, string key, string uid)
        {
            try
            {
                //// Restores the notes 
                this.firebase.Child("Persons").Child(uid).Child("Notes").Child(key).PutAsync(new NotesData() { Title = notes.Title, Notes = notes.Notes, IsDeleted = false, ColorNote = notes.ColorNote });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Pinned Notes
        /// </summary>
        /// <param name="notes">the notes.</param>
        /// <param name="key">key notes.</param>
        /// <param name="uid">user id.</param>
        public void PinnedNotes(NotesData notes, string key, string uid)
        {
            try
            {
                //// Restores the notes 
                this.firebase.Child("Persons").Child(uid).Child("Notes").Child(key).PutAsync(new NotesData() { Title = notes.Title, Notes = notes.Notes, IsPinned = true, ColorNote = notes.ColorNote });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// the pinned notes.
        /// </summary>
        /// <param name="notes">The notes.</param>
        /// <param name="key">The key.</param>
        /// <param name="uid">The id.</param>
        public void UnPinnedNotes(NotesData notes, string key, string uid)
        {
            try
            {
                //// Restores the notes and unpins it
                this.firebase.Child("Persons").Child(uid).Child("Notes").Child(key).PutAsync(new NotesData() { Title = notes.Title, Notes = notes.Notes, IsPinned = false, ColorNote = notes.ColorNote });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Adds the location.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="notes">The notes.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public async void AddLocation(string key, NotesData notes, string latitude, string longitude)
        {
            //// Gets the current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            //// Updates the notesdata by fetching the current loaction
            await this.firebase.Child("Persons").Child(userid).Child("Notes").Child(key).PutAsync(new NotesData()
            {
                Title = notes.Title,
                Notes = notes.Notes,
                ColorNote = notes.ColorNote,
                LabelData = notes.LabelData,
                // Area = notes.Area,
                Latitude = latitude,
                Longitude = longitude,
            });
        }

        /// <summary>
        /// Adds the location area.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="notes">The notes.</param>
        /// <param name="address">The address.</param>
        public async void AddLocationArea(string key, NotesData notes, string address, string latitude, string longitude)
        {
            //// Gets the current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            //// Updates the notesdata by fetching the loaction from the user
            await this.firebase.Child("Persons").Child(userid).Child("Notes").Child(key).PutAsync(new NotesData()
            {
                Title = notes.Title,
                Notes = notes.Notes,
                ColorNote = notes.ColorNote,
                LabelData = notes.LabelData,
                Latitude = latitude,
                Longitude = longitude,
                Area = address
            });
        }

        //public async Task GetImage(string imageSource)
        //{
        //    var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
        //    SignUpUserData signUp = await GetUser();
        //    if(userid!=null && signUp!=null)
        //    {
        //        await firebase.Child("Persons").Child(userid).Child("userinfo").PutAsync<SignUpUserData>(new SignUpUserData()
        //        {
        //            FirstName=signUp.FirstName,
        //            LastName=signUp.LastName,
        //            Email=signUp.Email,
        //            imageurl=imageSource
        //        });
        //    }

        //}

        //public async Task<SignUpUserData> GetUser()
        //{
        //    try
        //    {
        //        string userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
        //        if (userid != null)
        //        {
        //            SignUpUserData sign = await firebase.Child("Persons").Child(userid).Child("userinfo").OnceSingleAsync<SignUpUserData>();
        //            return sign;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    return null;
        //}

        public async void GetImage(string imageSource)
        {
            string userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            await this.firebase.Child("Persons").Child(userid).Child("Profile").PostAsync(new ProfileModel()
            {
               imageurl=imageSource,
            });
        }

        public async Task<List<ProfileModel>> GetProfilePic()
        {
            //// Gets the current user id
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            //// returns all the person contained in the list
            return (await this.firebase
              .Child("Persons").Child(userid).Child("Profile").OnceAsync<ProfileModel>()).Select(item => new ProfileModel
              {
                  imageurl = item.Object.imageurl,
                  ProfileKey = item.Key
              }).ToList();
        }   
    }
}
