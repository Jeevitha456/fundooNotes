// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrashPage.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------

namespace Fundoo.View.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Trash Page
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class TrashPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrashPage"/> class.
        /// </summary>
        public TrashPage()
        {
           this.InitializeComponent();
        }
    }
}