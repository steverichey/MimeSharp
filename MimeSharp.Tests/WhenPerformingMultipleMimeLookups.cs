// <copyright file="WhenPerformingMultipleMimeLookups.cs" company="Float">
// Copyright (c) 2015 Ujjwol Lamichhane and 2020 Float, LLC. All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using Xunit;

namespace MimeSharp.Tests
{
    public class WhenPerformingMultipleMimeLookups
    {
        [Fact]
        public void SupportsMakingMultipleLookups()
        {
            var firstResult = Mime.Lookup("test.html");
            var secondResult = Mime.Lookup("test.pdf");
            Assert.NotNull(firstResult);
            Assert.NotEmpty(firstResult);
            Assert.NotNull(secondResult);
            Assert.NotEmpty(secondResult);
        }
    }
}
