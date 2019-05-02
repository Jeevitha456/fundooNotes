// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Collab.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Collaborator class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Collab : ContentPage
    {
        Login login = new Login();

        /// <summary>
        /// Initializes a new instance of the <see cref="Collab"/> class.
        /// </summary>
        public Collab()
        {
          this.InitializeComponent();
           
        }
     
  
      

       
    }
}