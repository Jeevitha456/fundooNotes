// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemindersPage.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------

namespace Fundoo.View.Pages
{
    using Plugin.InputKit.Shared.Controls;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    ///  Notes Page Class
    /// </summary>
    public partial class RemindersPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemindersPage"/> class.
        /// </summary>
        public RemindersPage()
        {
            this.InitializeComponent();
        }
        static readonly Random rnd = new Random();
     

        private void RandomizeColors(object sender, EventArgs e)
        {
            var colors = typeof(Color).GetFields();
            var color = (Color)colors[rnd.Next(colors.Length)].GetValue(null);
            foreach (var view in group.Children)
            {
                if (view is RadioButton rb)
                {
                    rb.Color = color;
                }
            }
        }

        private void ChangePosition(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                if (rb.LabelPosition == Plugin.InputKit.Shared.LabelPosition.After)
                {
                    rb.LabelPosition = Plugin.InputKit.Shared.LabelPosition.Before;
                }
                else
                {
                    rb.LabelPosition = Plugin.InputKit.Shared.LabelPosition.After;
                }
            }
        }
    }
}