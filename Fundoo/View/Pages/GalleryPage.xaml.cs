// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GalleryPage.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using global::Firebase.Storage;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Gallery Page class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GalleryPage : ContentPage
    {
        /// <summary>
        /// The file
        /// </summary>
        private MediaFile file;

        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryPage"/> class.
        /// </summary>
        public GalleryPage()
        {
            this.InitializeComponent();
            imgBanner.Source = ImageSource.FromResource("XamarinFirebase.images.banner.png");
            imgChoosed.Source = ImageSource.FromResource("XamarinFirebase.images.default.jpg");
        }

        /// <summary>
        /// Stores the images.
        /// </summary>
        /// <param name="imageStream">The image stream.</param>
        /// <returns>returns task</returns>
        public async Task<string> StoreImages(Stream imageStream)
        {
            //// Stores the image in firebase storage
            var stroageImage = await new FirebaseStorage("fundooapp-50c31.appspot.com")
                .Child("XamarinMonkeys")
                .Child("image.jpg")
                .PutAsync(imageStream);
            string imgurl = stroageImage;
            return imgurl;
        }

        /// <summary>
        /// Handles the Clicked event of the Button Store control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void BtnStore_Clicked(object sender, EventArgs e)
        {
            //// Gets the file path
            await this.StoreImages(this.file.GetStream());
        }

        /// <summary>
        /// Handles the Clicked event of the Button Pick control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void BtnPick_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                this.file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });

                //// Checks that the file choosen is not null
                if (this.file == null)
                {
                    return;
                }

                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
                await this.StoreImages(this.file.GetStream());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}