using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UWP_API.Model;

namespace UWP_API.Services
{
    class UserService
    {
        private static readonly string MemberInfoUrl = "https://2-dot-backup-server-002.appspot.com/_api/v2/members/information";

        public async Task<User> GetUserInfo(string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var response = await httpClient.GetAsync(MemberInfoUrl);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                var stringContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(stringContent);
            }
            return null;
        }
    }
}
