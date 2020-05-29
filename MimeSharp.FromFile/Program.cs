// <copyright file="Program.cs" company="Float">
// Copyright (c) 2015 Ujjwol Lamichhane and 2020 Float, LLC. All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System;
using Newtonsoft.Json;

namespace MimeSharp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("MimeSharp example usage: ");
            Console.WriteLine("MIME type for {0} is {1}", "song.ogg", Mime.Lookup(@"song.ogg"));
            Console.WriteLine("The default MIME type is {0}", Mime.DefaultType);
            Console.WriteLine("File extensions that match the mime type {0} are :", "audio/ogg");
            Console.WriteLine(JsonConvert.SerializeObject(Mime.Extension("audio/ogg")));

            foreach (var arg in args)
            {
                Console.WriteLine(JsonConvert.SerializeObject(Mime.Extension(arg)));
            }
        }
    }
}
