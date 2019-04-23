using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fundoo.Model
{
    public class SetColor:ContentPage
    {
        public void GetColor(NotesData note, Frame frame)
        {
            if (note.ColorNote.Equals("Red"))
            {
                frame.BackgroundColor = Color.Red;
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
                frame.BackgroundColor = Color.Green;
                return;
            }

            if (note.ColorNote.Equals("Blue"))
            {
                frame.BackgroundColor = Color.Blue;
                return;
            }

            if (note.ColorNote.Equals("Teal"))
            {
                frame.BackgroundColor = Color.Teal;
                return;
            }

            if (note.ColorNote.Equals("DarkBlue"))
            {
                frame.BackgroundColor = Color.DarkBlue;
                return;
            }

            if (note.ColorNote.Equals("Purple"))
            {
                frame.BackgroundColor = Color.Purple;
                return;
            }
            if (note.ColorNote.Equals("Pink"))
            {
                frame.BackgroundColor = Color.Pink;
                return;
            }
            if (note.ColorNote.Equals("Brown"))
            {
                frame.BackgroundColor = Color.Brown;
                return;
            }
            if (note.ColorNote.Equals("Gray"))
            {
                frame.BackgroundColor = Color.Gray;
                return;
            }
        }

        public static string GetHexColor(NotesData note)
        {
            if (note.ColorNote.Equals("Red"))
            {
                return "ff0000";
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

                return "00cd00";
            }

            if (note.ColorNote.Equals("Blue"))
            {

                return "0000ff";
            }

            if (note.ColorNote.Equals("Teal"))
            {

                return "f5deb3";
            }

            if (note.ColorNote.Equals("DarkBlue"))
            {

                return "00688b";
            }

            if (note.ColorNote.Equals("Purple"))
            {

                return "a020f0";
            }
            if (note.ColorNote.Equals("Pink"))
            {

                return "ffc0cb";
            }
            if (note.ColorNote.Equals("Brown"))
            {

                return "a52a2a";
            }
            if (note.ColorNote.Equals("Gray"))
            {

                return "bebebe";
            }

            return "ffffff";
        }
    }
}
