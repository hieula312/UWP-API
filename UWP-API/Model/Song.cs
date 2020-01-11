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
            //Name
            if (string.IsNullOrEmpty(this.name))
            {
                errors.Add("nameErr", "Name must be filled");
            }
            //Description
            if (string.IsNullOrEmpty(this.description))
            {
                errors.Add("descriptionErr", "Description must be filled");
            }
            //Author
            if (string.IsNullOrEmpty(this.author))
            {
                errors.Add("authorErr", "Author must be filled");
            }
            //Singer
            if (string.IsNullOrEmpty(this.singer))
            {
                errors.Add("singerErr", "Singer must be filled");
            }
            //Link
            if (string.IsNullOrEmpty(this.link))
            {
                errors.Add("linkErr", "Link must be filled");
            }
            else
            {
                if (!Regex.IsMatch(this.link, "^(http|https)://"))
                {
                    errors.Add("linkErr", "Link format is incorrect");
                }
            }
            //Thumbnail
            if (string.IsNullOrEmpty(this.thumbnail))
            {
                errors.Add("thumbnailErr", "Thumbnail must be filled");
            }
            else
            {
                if (!Regex.IsMatch(this.thumbnail, "^(http|https)://"))
                {
                    errors.Add("thumbnailErr", "Thumbnail format is incorrect");
                }
            }
            return errors;
        }
    }
}
