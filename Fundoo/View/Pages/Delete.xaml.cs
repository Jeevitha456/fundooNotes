using Fundoo.Firebase;
using Fundoo.Interface;
using Fundoo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fundoo.View.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Delete : ContentPage
	{
        string val = null;
		public Delete (string value)
        {
            val = value;
            InitializeComponent ();
            DeleteData();
		}

        public async void DeleteData()
        {
            try
            {
                FirebaseHelper firebaseHelper = new FirebaseHelper();
                var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
                NotesData notesData = await firebaseHelper.GetNotesData(this.val, userid);
                
                txtTitle.Text = notesData.Title;
                txtNotes.Text = notesData.Notes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            try
            {
                FirebaseHelper firebaseHelper = new FirebaseHelper();
                var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
                NotesData notes = new NotesData()
                {
                    Title = txtTitle.Text,
                    Notes = txtNotes.Text
                };
                firebaseHelper.DeleteForever(notes, this.val, userid);

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }

        }
    }
}