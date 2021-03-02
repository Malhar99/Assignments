using ConsoleApp.extension;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ExtensionMethod.Test
{
    public class ExtensionMethodTest
    {
        [Fact]
        public void Test_ChangeCase()
        {
            // Arrange
            var inputString = "VIRen";
            var outputString = "viren";
            // Act
            var newString = inputString.ChangeCase();
            // Assert
            Assert.Equal(newString, outputString);
        }
        [Fact]
        public void Test_ChangeToTitleCase()
        {
            // Arrange
            var inputString = "viren is in MCA";
            var outputString = "Viren Is In MCA";
            // Act
            var newString = inputString.toTitleCase();
            // Assert
            Assert.Equal(newString, outputString);
        }
        [Fact]
        public void Test_IsLowerCaseString()
        {
            // Arrange
            var inputString = "virennanda";
            // Act
            var newString = inputString.isLowerCase();
            // Assert
            Assert.True(newString);
        }
        [Fact]
        public void Test_IsUpperCaseString()
        {
            // Arrange
            var inputString = "VIREN";
            // Act
            var newString = inputString.IsUpperCaseString();
            // Assert
            Assert.True(newString);
        }
        [Fact]
        public void Test_DoCapitalize()
        {
            // Arrange
            var inputString = "viren";
            var outputString = "VIREN";
            // Act
            var newString = inputString.doCapitalize();
            // Assert
            Assert.Equal(newString, outputString);
        }
        [Theory]
        [InlineData("56", true)]
        [InlineData("12d", false)]
        public void Test_IsValidNumericValue(string inputString, bool result)
        {
            // Arrange

            // Act
            var newString = inputString.isValidNumericValue();
            // Assert
            Assert.Equal(newString, result);
        }
        [Fact]
        public void Test_RemoveLastCharacter()
        {
            // Arrange
            var inputString = "Viren";
            var outputString = "Vire";
            // Act
            var newString = inputString.removeLastCharacter();
            // Assert
            Assert.Equal(newString, outputString);
        }
        [Fact]
        public void Test_WordCount()
        {
            // Arrange
            var inputString = "Viren Nanda";
            var count = 2;
            // Act
            var newString = inputString.wordCount();
            // Assert
            Assert.Equal(newString, count);
        }
        [Fact]
        public void Test_StringToInteger()
        {
            // Arrange
            var inputString = "78765";
            var output = 123;
            // Act
            var newString = inputString.stringToInteger();
            // Assert
            Assert.Equal(newString, output);
        }
    }
}
