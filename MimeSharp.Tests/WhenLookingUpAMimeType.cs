// <copyright file="WhenLookingUpAMimeType.cs" company="Float">
// Copyright (c) 2015 Ujjwol Lamichhane and 2020 Float, LLC. All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using Xunit;

namespace MimeSharp.Tests
{
    public class WhenLookingUpAMimeType
    {
        [Fact]
        public void HtmlExtensionShouldHaveCorrectMimeType()
        {
            var result = Mime.Lookup("test.html");
            Assert.Equal("text/html", result);
        }

        [Fact]
        public void HtmExtensionIsCorrectlyIdentified()
        {
            var result = Mime.Lookup("test.htm");
            Assert.Equal("text/html", result);
        }
    }
}
