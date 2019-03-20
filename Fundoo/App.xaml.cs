// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Fundoo
{
    using Fundoo.View;
    using Xamarin.Forms;

    /// <summary>
    /// Application 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Application" />
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.MainPage = new NavigationPage(new Login());
        }

        /// <summary>
        /// Application developers override this method to perform actions when the application starts.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Application developers override this method to perform actions when the application enters the sleeping state.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Application developers override this method to perform actions when the application resumes from a sleeping state.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
