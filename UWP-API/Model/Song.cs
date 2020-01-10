using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UWP_API.Model
{
    class Song
    {
        public String name { get; set; }
        public String description { get; set; }
        public String author { get; set; }
        public String singer { get; set; }
        public String thumbnail { get; set; }
        public String link { get; set; }

        public Dictionary<string, string> CheckValidate()
        {
            var errors = new Dictionary<string, string>();
            //Password
            if (string.IsNullOrEmpty(this.name))
            {
                errors.Add("name", "Name must be filled");
            }

            if (string.IsNullOrEmpty(this.description))
            {
                errors.Add("description", "Description must be filled");
            }

            if (string.IsNullOrEmpty(this.author))
            {
                errors.Add("author", "Author must be filled");
            }

            if (string.IsNullOrEmpty(this.singer))
            {
                errors.Add("singer", "Singer must be filled");
            }

            if (Regex.IsMatch(this.link, "^(http|https)://"))
            {
                errors.Add("link", "Link format is incorrect");
            }
            else
            {
                if (string.IsNullOrEmpty(this.link))
                {
                    errors.Add("link", "Link must be filled");
                }
            }

            if (Regex.IsMatch(this.thumbnail, "^(http|https)://"))
            {
                errors.Add("thumbnail", "Thumbnail format is incorrect");
            }
            else
            {
                if (string.IsNullOrEmpty(this.link))
                {
                    errors.Add("thumbnail", "Link must be filled");
                }
            }

            return errors;
        }
    }
}
