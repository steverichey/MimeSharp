// <copyright file="Mime.cs" company="Float">
// Copyright (c) 2015 Ujjwol Lamichhane and 2020 Float, LLC. All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MimeSharp
{
    /// <summary>
    /// Manages mime types.
    /// </summary>
    public static class Mime
    {
        static readonly IDictionary<string, List<string>> ApacheMimes = new Dictionary<string, List<string>>();

        static Mime()
        {
            var allApacheMimeTypes = ApacheMimeTypes.AllMimeTypes;

            using (var stringReader = new StringReader(allApacheMimeTypes))
            {
                string line;

                while ((line = stringReader.ReadLine()) != null)
                {
                    // remove comments
                    var currentLine = Regex.Replace(line, @"\s*#.*|^\s*|\s*$/g", string.Empty);

                    // split them by whitespace
                    var stripWhiteSpaceRegEx = new Regex(@"\s+", RegexOptions.None);

                    if (!string.IsNullOrWhiteSpace(currentLine))
                    {
                        var matches = stripWhiteSpaceRegEx.Split(currentLine);

                        // add the mime type and extension to the dictionary
                        // mime-type is the key and value is the list of extensons it is associated with
                        // e.g. {"application/mathematica":["ma","nb","mb"]}
                        ApacheMimes.Add(matches.First(), matches.Skip(1).ToList());
                    }
                }
            }
        }

        /// <summary>
        /// Gets the default mime type.
        /// </summary>
        /// <value>The default mime type.</value>
        public static string DefaultType => "application/octet-stream";

        /// <summary>
        /// Look up the mime type for a given file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <returns>The mime type for the file.</returns>
        public static string Lookup(string filePath)
        {
#pragma warning disable CA1308 // Normalize strings to uppercase
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
#pragma warning restore CA1308 // Normalize strings to uppercase

            // return default type if there is no extension
            if (string.IsNullOrWhiteSpace(extension))
            {
                return DefaultType;
            }

            // remove dot from extenstion to lookup in the dictionary
            extension = extension.Substring(1);

            // Get an exact match if possible
            var mimeType = ApacheMimes.FirstOrDefault(x => x.Value.Exists(m => m == extension)).Key;

            if (!string.IsNullOrWhiteSpace(mimeType))
            {
                return mimeType;
            }

            // Get a close match
            mimeType = ApacheMimes.FirstOrDefault(x => x.Value.Exists(m => m.Contains(extension))).Key;

            if (string.IsNullOrWhiteSpace(mimeType))
            {
                return DefaultType;
            }

            return mimeType;
        }

        /// <summary>
        /// Gets the extension for the given mime type.
        /// </summary>
        /// <param name="mimeType">The mime type.</param>
        /// <returns>The extension.</returns>
        public static IList<string> Extension(string mimeType)
        {
            var extensions = ApacheMimes.FirstOrDefault(x => x.Key == mimeType).Value;

            if (extensions == null)
            {
                return new List<string>();
            }

            return extensions;
        }
    }
}
