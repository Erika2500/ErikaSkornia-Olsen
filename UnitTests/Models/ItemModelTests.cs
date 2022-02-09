using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Mine.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class ItemModelTests
    {
        [Test]
        public void ItemModel_Constructor_Valid_Default_Should_Pass
()
        {
            // Arrange
            // Act
            var result = new ItemModel();
            // Reset
            // Assert 
            Assert.IsNotNull(result);
        }
    }
}
