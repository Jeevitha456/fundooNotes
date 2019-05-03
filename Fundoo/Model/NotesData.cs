// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesData.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Notes Data class
    /// </summary>
    public class NotesData
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is archive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is archive; otherwise, <c>false</c>.
        /// </value>
        public bool IsArchive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pinned.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is pinned; otherwise, <c>false</c>.
        /// </value>
        public bool IsPinned { get; set; }

        /// <summary>
        /// Gets or sets the color note.
        /// </summary>
        /// <value>
        /// The color note.
        /// </value>
        public string ColorNote { get; set; }

        /// <summary>
        /// Gets or sets the label data.
        /// </summary>
        /// <value>
        /// The label data.
        /// </value>
        public IList<string> LabelData { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>
        /// The area.
        /// </value>
        public string Area { get; set; }
    }
}