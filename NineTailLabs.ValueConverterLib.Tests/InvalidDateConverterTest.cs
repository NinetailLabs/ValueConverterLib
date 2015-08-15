using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NineTailLabs.ValueConverterLib.Tests
{
    [TestClass]
    public class InvalidDateConverterTest
    {
        /// <summary>
        /// Test the Convert method when passing a valid DateTime
        /// </summary>
        [TestMethod]
        public void TestConvertValidDate()
        {
            var instance = new InvalidDateConverter();
            var date = DateTime.Now;
            var retVal = instance.Convert(date, typeof (string), "Unknown", CultureInfo.InvariantCulture);
            Assert.AreEqual(date.ToString(CultureInfo.InvariantCulture), retVal);
        }

        /// <summary>
        /// Test the Convert method when passing a DateTime.MinValue and DateTime.MaxValue
        /// </summary>
        [TestMethod]
        public void TestConvertInvalidDate()
        {
            var instance = new InvalidDateConverter();
            var date = DateTime.MaxValue;
            var retVal = instance.Convert(date, typeof (string), "Unknown", CultureInfo.InvariantCulture);
            Assert.AreEqual("Unknown", retVal);

            var minDate = DateTime.MinValue;
            var minRet = instance.Convert(minDate, typeof (string), "Unknown", CultureInfo.InvariantCulture);
            Assert.AreEqual("Unknown", minRet);
        }

        /// <summary>
        /// Test the Convert method when passing a null date
        /// </summary>
        [TestMethod]
        public void TestConvertNullDate()
        {
            var instance = new InvalidDateConverter();
            var retVal = instance.Convert(null, typeof (string), "Unknown", CultureInfo.InvariantCulture);
            Assert.AreEqual("{Binding.DoNothing}", retVal.ToString());
        }

        /// <summary>
        /// Test the Convert method when passing a null parameter
        /// </summary>
        [TestMethod]
        public void TestConvertNullParameter()
        {
            var instance = new InvalidDateConverter();
            var date = DateTime.MaxValue;
            var retVal = instance.Convert(date, typeof (string), null, CultureInfo.InvariantCulture);
            Assert.AreEqual("-", retVal);
        }

        /// <summary>
        /// Test the ConvertBack method when passing a valid DateTime string
        /// </summary>
        [TestMethod]
        public void TestConvertBackValidDate()
        {
            var instance = new InvalidDateConverter();
            var date = DateTime.Now;
            var stringDate = date.ToString(CultureInfo.InvariantCulture);
            var retVal = instance.ConvertBack(stringDate, typeof (DateTime), "Unknown", CultureInfo.InvariantCulture);
            var retDate = retVal as DateTime?;
            Assert.IsNotNull(retDate);
            Assert.AreEqual(date.ToString(CultureInfo.InvariantCulture), ((DateTime)retDate).ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Test the ConvertBack method when passing a '-' string
        /// </summary>
        [TestMethod]
        public void TestConvertBackInvalidDate()
        {
            var instance = new InvalidDateConverter();
            var retDate = instance.ConvertBack("Unknown", typeof (DateTime),
                "Unknown", CultureInfo.InvariantCulture);
            Assert.AreEqual(DateTime.MinValue, (DateTime)retDate);
        }

        /// <summary>
        /// Test the ConvertBack methods when passing a null
        /// </summary>
        [TestMethod]
        public void TestConvertBackNullValue()
        {
            var instance = new InvalidDateConverter();
            var retDate = instance.ConvertBack(null, typeof (DateTime), "Unknown", CultureInfo.InvariantCulture);
            Assert.AreEqual("{Binding.DoNothing}", retDate.ToString());
        }

        /// <summary>
        /// Test the ConvertBack method when passing a null parameter
        /// </summary>
        [TestMethod]
        public void TestConvertBackNullParameter()
        {
            var instance = new InvalidDateConverter();
            var retDate = instance.ConvertBack("-", typeof (DateTime), null, CultureInfo.InvariantCulture);
            Assert.AreEqual(DateTime.MinValue, (DateTime)retDate);
        }

        /// <summary>
        /// Attempt to convert a non-date string back into a DateTime
        /// </summary>
        [TestMethod]
        public void TestNonDateStringConvertBack()
        {
            var instance = new InvalidDateConverter();
            var retDate = instance.ConvertBack("Hello World", typeof (DateTime), null, CultureInfo.InvariantCulture);
            Assert.AreEqual("{DependencyProperty.UnsetValue}", retDate.ToString());
        }
    }
}
