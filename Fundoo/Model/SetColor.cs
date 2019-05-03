// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetColor.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.Model
{
    using Xamarin.Forms;

    /// <summary>
    /// Set Color class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public class SetColor : ContentPage
    {
        /// <summary>
        /// Gets the color of the hexadecimal.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns>returns string</returns>
        public static string GetHexColor(NotesData note)
        {
            if (note.ColorNote.Equals("Red"))
            {
                return "DC143C";
            }

            if (note.ColorNote.Equals("Orange"))
            {
                return "ffa500";
            }

            if (note.ColorNote.Equals("Yellow"))
            {
                return "ffff00";
            }

            if (note.ColorNote.Equals("Green"))
            {
                return "98FB98";
            }

            if (note.ColorNote.Equals("Blue"))
            {
                return "87CEFA";
            }

            if (note.ColorNote.Equals("Teal"))
            {
                return "7FFFD4";
            }

            if (note.ColorNote.Equals("DarkBlue"))
            {
                return "6495ED";
            }

            if (note.ColorNote.Equals("Purple"))
            {
                return "BOC4DE";
            }

            if (note.ColorNote.Equals("Pink"))
            {
                return "ffc0cb";
            }

            if (note.ColorNote.Equals("Brown"))
            {
                return "BC8F8F";
            }

            if (note.ColorNote.Equals("Gray"))
            {
                return "D3D3D3";
            }

            return "ffffff";
        }

        /// <summary>
        /// Gets the color.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="frame">The frame.</param>
        public void GetColor(NotesData note, Frame frame)
        {
            if (note.ColorNote.Equals("Red"))
            {
                frame.BackgroundColor = Color.Crimson;
                return;
            }

            if (note.ColorNote.Equals("Orange"))
            {
                frame.BackgroundColor = Color.Orange;
                return;
            }

            if (note.ColorNote.Equals("Yellow"))
            {
                frame.BackgroundColor = Color.Yellow;
                return;
            }

            if (note.ColorNote.Equals("Green"))
            {
                frame.BackgroundColor = Color.PaleGreen;
                return;
            }

            if (note.ColorNote.Equals("Blue"))
            {
                frame.BackgroundColor = Color.LightSkyBlue;
                return;
            }

            if (note.ColorNote.Equals("Teal"))
            {
                frame.BackgroundColor = Color.Aquamarine;
                return;
            }

            if (note.ColorNote.Equals("DarkBlue"))
            {
                frame.BackgroundColor = Color.CornflowerBlue;
                return;
            }

            if (note.ColorNote.Equals("Purple"))
            {
                frame.BackgroundColor = Color.LightSteelBlue;
                return;
            }

            if (note.ColorNote.Equals("Pink"))
            {
                frame.BackgroundColor = Color.Pink;
                return;
            }

            if (note.ColorNote.Equals("Brown"))
            {
                frame.BackgroundColor = Color.RosyBrown;
                return;
            }

            if (note.ColorNote.Equals("Gray"))
            {
                frame.BackgroundColor = Color.LightGray;
                return;
            }
        }
    }
}
