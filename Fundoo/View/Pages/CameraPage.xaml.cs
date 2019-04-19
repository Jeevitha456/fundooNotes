// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CameraPage.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using global::Firebase.Storage;
    using Plugin.Media;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Camera Page
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraPage"/> class.
        /// </summary>
        public CameraPage()
        {
          this.InitializeComponent();
        }

        /// <summary>
        /// Stores the images.
        /// </summary>
        /// <param name="imageStream">The image stream.</param>
        /// <returns>returns task</returns>
        public async Task<string> StoreImages(Stream imageStream)
        {
            var stroageImage = await new FirebaseStorage("fundooapp-50c31.appspot.com")
                .Child("XamarinMonkeys")
                .Child("image.jpg")
                .PutAsync(imageStream);
            string imgurl = stroageImage;
            return imgurl;
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsTakePhotoSupported && CrossMedia.Current.IsPickPhotoSupported)
            {
                await this.DisplayAlert("alert", "take photo not supported", "ok");
                return;
            }
            else
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Images",
                    Name = DateTime.Now + "_test.jpg",
                    SaveToAlbum = true
                });

                if (file == null)
                {
                    return;
                }

                await this.DisplayAlert("file path", file.Path, "ok");

                MyImage.Source = ImageSource.FromStream(() =>
                {
                    var steam = file.GetStream();
                    return steam;
                });

                await this.StoreImages(file.GetStream());
            }
        }
    }
}