using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TurboShop.validations
{
    internal class Validation
    {
        public static bool Required(string data)
        {
            return !string.IsNullOrEmpty(data);
        }

        public static bool Email(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        public static bool Unique<T>(List<T> referenceData, Func<T, string> fieldSelector, string data)
        {
            foreach (var item in referenceData)
            {
                string fieldValue = fieldSelector(item);

                if (!string.IsNullOrEmpty(fieldValue) && fieldValue.Equals(data, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Min(string data)
        {
            return !string.IsNullOrEmpty(data) && data.Length >= 6;
        }

        public static bool Some(string rePass, string pass)
        {
            return rePass.Equals(pass);
        }

        public static bool Number(string data)
        {
            return decimal.TryParse(data, out _);
        }

        public static bool NonNegative(decimal number)
        {
            return number >= 0;
        }

        public static bool ValidYear(string year)
        {
            return !string.IsNullOrEmpty(year) && Regex.IsMatch(year, @"^\d{4}$");
        }

        public static bool Phone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return false;

            string phonePattern = @"^\+?[0-9]{9,15}$";
            return Regex.IsMatch(phone, phonePattern);
        }

        public static bool EnumValueExists<TEnum>(int value) where TEnum : Enum
        {
            return Enum.IsDefined(typeof(TEnum), value);
        }

        public static bool ItemExists<T>(List<T> items, int id, Func<T, int> idSelector)
        {
            foreach (var item in items)
            {
                if (idSelector(item) == id)
                {
                    return true;
                }
            }
            return false;
        }



    }
}
