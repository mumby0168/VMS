using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using Services.Common.Extensions;

namespace Services.Common.Validation
{
    public static class Validator
    {
        public static bool IsPostCodeValid(string postCode) 
            => !postCode.IsEmpty() && Regex.IsMatch(postCode.Replace(" ", ""), 
                   @"([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([AZa-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9]?[A-Za-z]))))[0-9][A-Za-z]{2})");

        public static bool IsUkPhoneNumberValid(string phoneNumber)
            => !phoneNumber.IsEmpty() && Regex.IsMatch(phoneNumber,
                @"^(((\+44\s?\d{4}|\(?0\d{4}\)?)\s?\d{3}\s?\d{3})|((\+44\s?\d{3}|\(?0\d{3}\)?)\s?\d{3}\s?\d{4})|((\+44\s?\d{2}|\(?0\d{2}\)?)\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$");

        public static bool IsEmailValid(string email)
        {
            try
            {
                new MailAddress(email);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
            
    }
}
