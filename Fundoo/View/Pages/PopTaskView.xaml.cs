// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PopTaskView.xaml.cs" company="Bridgelabz">
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
    using Rg.Plugins.Popup.Pages;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Pop Task View
    /// </summary>
    /// <seealso cref="Rg.Plugins.Popup.Pages.PopupPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopTaskView : PopupPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PopTaskView"/> class.
        /// </summary>
        public PopTaskView()
        {
          this.InitializeComponent();
        }
    }
}