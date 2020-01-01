using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
