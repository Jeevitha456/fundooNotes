using Fundoo.Firebase;
using Fundoo.Interface;
using Fundoo.Model;
using Rg.Plugins.Popup.Services;
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
	public partial class UpdateNote : ContentPage
	{
        string val = null;

		public UpdateNote (string value)
		{
            val = value;
			InitializeComponent ();
            UpdateData();
		}

        public async void UpdateData()
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
        
        protected override bool OnBackButtonPressed()
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
                firebaseHelper.UpdateNotes(notes, this.val, userid);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return base.OnBackButtonPressed(); 
        }
        
        

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopTaskView());
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            NotesData notes = new NotesData()
            {
                Title = txtTitle.Text,
                Notes = txtNotes.Text,
                IsDeleted = true
            };
            firebaseHelper.DeleteNotes(notes, this.val, userid);

        }

        private void TxtArchieve_Clicked(object sender, EventArgs e)
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();
            var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            NotesData notes = new NotesData()
            {
                Title = txtTitle.Text,
                Notes = txtNotes.Text,
                IsArchive = true
            };
            firebaseHelper.ArchiveNotes(notes, this.val, userid);
        }
    }
}