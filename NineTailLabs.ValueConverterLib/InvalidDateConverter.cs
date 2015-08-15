using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NineTailLabs.ValueConverterLib
{
    /// <summary>
    /// 
    /// </summary>
    public class InvalidDateConverter : IValueConverter
    {
        /// <summary>
        /// Check if a date is valid (not the max or min date) and if it isn't return a value to indicate that the date is unknown
        /// </summary>
        /// <param name="value">DateTime value</param>
        /// <param name="targetType">String</param>
        /// <param name="parameter">String to return if date is invalid. If blank returns '-'</param>
        /// <param name="culture">Culture information for the date</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Binding.DoNothing;

            var returnValue = "-";
            if (parameter != null)
            {
                returnValue = parameter.ToString();
            }

            var dateValue = value as DateTime?;
            if (dateValue != null && dateValue != DateTime.MaxValue && dateValue != DateTime.MinValue)
            {
                    returnValue = ((DateTime)dateValue).ToString(culture);
            }

            return returnValue;
        }

        /// <summary>
        /// Check if the date string is valid (not a dash or spesified invalid) and returns it as a date.
        /// For invalid dates DateTime.MinValue is returned
        /// </summary>
        /// <param name="value">Date string</param>
        /// <param name="targetType">DateTime</param>
        /// <param name="parameter">String to return if date is invalid. If blank uses '-'</param>
        /// <param name="culture">Culture information for the date</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Binding.DoNothing;

            var returnValue = DateTime.MinValue;
            var paramValue = "-";
            if (parameter != null)
            {
                paramValue = parameter.ToString();
            }

            if (value.ToString() == paramValue) return returnValue;
            try
            {
                returnValue = DateTime.Parse(value.ToString(), culture);
            }
            catch (Exception)
            {
                return DependencyProperty.UnsetValue;
            }
            return returnValue;
        }
    }
}