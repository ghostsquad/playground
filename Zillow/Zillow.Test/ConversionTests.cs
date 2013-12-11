namespace Zillow.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Ploeh.AutoFixture;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ConversionTests
    {
        private const long BasicExpected = 123;

        private const string BasicValue = "123";

        [TestMethod]
        public void GivenZillowTestWhenIndividualIntExpectLong()
        {
            // act
            var actual = Conversions.StringToLong(BasicValue);

            // assert
            Assert.AreEqual<long>(BasicExpected, actual);
        }

        [TestMethod]
        public void GivenZillowTestWhenRoundedExpectLong()
        {
            // act
            var actual = Conversions.StringToLong(BasicValue, Conversions.ConversionMethods.Rounded);

            // assert
            Assert.AreEqual<long>(BasicExpected, actual);
        }

        [TestMethod]
        public void GivenZillowTestWhenTryParseExpectLong()
        {
            // act
            var actual = Conversions.StringToLong(BasicValue, Conversions.ConversionMethods.TryParse);

            // assert
            Assert.AreEqual<long>(BasicExpected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GivenNotImplementedMethodExpectException()
        {
            // act
            Conversions.StringToLong(BasicValue, Conversions.ConversionMethods.NotImplementedMethod);
        }

        [TestMethod]
        public void GivenLongMaxValue()
        {
            // arrange
            var expected = long.MaxValue;
            var value = expected.ToString();

            // act
            var actual = Conversions.StringToLong(value);

            // assert
            Assert.AreEqual<long>(expected, actual);
        }

        [TestMethod]
        public void GivenNegativeInteger()
        {
            // arrange
            var fixture = new Fixture();
            long expected = 0 - fixture.Create<uint>();
            var value = expected.ToString();

            // act
            var actual = Conversions.StringToLong(value);

            // assert
            Assert.AreEqual<long>(expected, actual);
        }

        [TestMethod]
        public void GivenLongMinValue()
        {
            // arrange
            var expected = long.MinValue;
            var value = expected.ToString();

            // act
            var actual = Conversions.StringToLong(value);

            // assert
            Assert.AreEqual<long>(expected, actual);
        }

        [TestMethod]
        public void GivenZero()
        {
            // arrange
            long expected = 0;
            var value = expected.ToString();

            // act
            var actual = Conversions.StringToLong(value);

            // assert
            Assert.AreEqual<long>(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GivenNumberToBigExpectException()
        {
            // arrange
            var longMaxPlusOne = "9223372036854775808";

            // act
            Conversions.StringToLong(longMaxPlusOne);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GivenNumberTooSmallExpectException()
        {
            // arrange
            var longMinMinusOne = "-9223372036854775809";

            // act
            Conversions.StringToLong(longMinMinusOne);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenInCorrectFormatWhenIndividualIntegersExpectException()
        {
            // arrange
            var fixture = new Fixture();
            
            // default strings by autofixture will be in the format of guids
            var value = fixture.Create<string>();

            // act
            Conversions.StringToLong(value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenIncorrectDoubleDecimalFormatWhenIndividualIntegersExpectException()
        {
            // arrange
            var value = "123.456.789";            

            // act
            Conversions.StringToLong(value);
        }

        public void GivenIncorrectDoubleDecimalFormatWhenRoundedExpectException()
        {
            // arrange
            var value = "123.456.789";

            // act
            Conversions.StringToLong(value, Conversions.ConversionMethods.Rounded);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenIncorrectDoubleNegativeFormatWhenIndividualIntegersExpectException()
        {
            // arrange
            var value = "--1";

            // act
            Conversions.StringToLong(value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenIncorrectDoubleNegativeFormatWhenRoundedExpectException()
        {
            // arrange
            var value = "--1";

            // act
            Conversions.StringToLong(value, Conversions.ConversionMethods.Rounded);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenIncorrectDoubleNegativeFormatWhenTryParseExpectException()
        {
            // arrange
            var value = "--1";

            // act
            Conversions.StringToLong(value, Conversions.ConversionMethods.TryParse);
        }

        [TestMethod]
        public void GivenDoubleWhenRoundedExpectLong()
        {
            // arrange
            var fixture = new Fixture();
            var expectedAsDouble = fixture.Create<double>();
            var expected = (long)expectedAsDouble;
            var value = expectedAsDouble.ToString();

            // act
            var actual = Conversions.StringToLong(value, Conversions.ConversionMethods.Rounded);

            // assert
            Assert.AreEqual<long>(expected, actual);
        }

        [TestMethod]
        public void GivenNegativeDoubleWhenRoundedExpectLong()
        {
            // arrange
            var fixture = new Fixture();
            var expectedAsDouble = 0 - fixture.Create<double>();
            var expected = (long)expectedAsDouble;
            var value = expectedAsDouble.ToString();

            // act
            var actual = Conversions.StringToLong(value, Conversions.ConversionMethods.Rounded);

            // assert
            Assert.AreEqual<long>(expected, actual);
        }
    }
}
