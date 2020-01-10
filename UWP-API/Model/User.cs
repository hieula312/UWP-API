using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UWP_API.Pages;

namespace UWP_API.Model
{
    class User
    {
        public String id { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String password { get; set; }
        public String address { get; set; }
        public String phone { get; set; }
        public String avatar { get; set; }
        public int gender { get; set; }
        public String email { get; set; }
        public String birthday { get; set; }

        public Dictionary<string, string> CheckValidate()
        {
            var errors = new Dictionary<string, string>();
            //Password
            if (string.IsNullOrEmpty(this.password))
            {
                errors.Add("passwordErr", "Password must be filled");
            }
            //FisrtName
            if (string.IsNullOrEmpty(this.firstName))
            {
                errors.Add("fisrtNameErr", "First name must be filled");
            }
            //LastName
            if (string.IsNullOrEmpty(this.lastName))
            {
                errors.Add("lastNameErr", "Last name must be filled");
            }
            //Address
            if (string.IsNullOrEmpty(this.address))
            {
                errors.Add("addressErr", "Last name must be filled");
            }
            //Phone
            if (string.IsNullOrEmpty(this.phone))
            {
                errors.Add("phoneErr", "Phone must be filled");
            }
            else
            {
                if (!Regex.IsMatch(this.phone, "/((09|03|07|08|05)+([0-9]{8})\b)/g"))
                {
                    errors.Add("phoneErr", "Phone format is incorrect");
                }

            }
            //Email
            if (string.IsNullOrEmpty(this.email))
            {
                errors.Add("emailErr", "Email must be filled");
            }
            else
            {
                if (!Regex.IsMatch(this.email, "@gmail.com"))
                {
                    errors.Add("emailErr", "Email formar is incorrect");
                }
            }
            if ((DateTime.Now - Register.birthDayVal).TotalDays < 3650)
            {
                errors.Add("birthdayErr", "User must be over 10 years old");

            }else
            {
                if ((string.IsNullOrEmpty(this.birthday.ToString())))
                {
                    errors.Add("birthdayErr", "Birthday must be filled");
                }
            }
            return errors;
        }
    }
}
