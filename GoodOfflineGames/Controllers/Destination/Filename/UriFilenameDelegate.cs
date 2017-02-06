﻿using System;
using System.IO;

using Interfaces.Destination.Filename;

using Models.Separators;

namespace Controllers.Destination.Filename
{
    public class UriFilenameDelegate : IGetFilenameDelegate
    {
        public virtual string GetFilename(string source = null)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            var filename = Path.GetFileName(source);

            if (!filename.Contains(Separators.UriPart) &&
                !filename.Contains(Separators.QueryString))
                return filename;

            var uriParts = filename.Split(
                new string[] { Separators.UriPart },
                StringSplitOptions.RemoveEmptyEntries);

            var filenameWithQueryString = uriParts[uriParts.Length - 1];

            if (!filenameWithQueryString.Contains(Separators.QueryString))
                return filenameWithQueryString;

            var filenameParts = filenameWithQueryString.Split(
                new string[] { Separators.QueryString },
                StringSplitOptions.RemoveEmptyEntries);

            return filenameParts[filenameParts.Length - 1];
        }
    }
}