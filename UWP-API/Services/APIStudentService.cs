using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UWP_API.Model;

namespace UWP_API.Services
{
    class ApiStudentService : IUserService
    {
        private static string REGISTER_API_URL = "https://2-dot-backup-server-002.appspot.com/_api/v2/members";
        private static string CONTENT_TYPE = "application/json";
        public Task<User> Create(User user)
        {
            return PostStudent(user);
        }

        public Task<List<User>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetDetail(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string rollNumber)
        {
            throw new NotImplementedException();
        }
        private async Task<User> PostStudent(User member)
        {
            // chuyển đối tượng member thành định dạng json.
            var userJson = JsonConvert.SerializeObject(member);
            // quá trình đóng gói dữ liệu trước khi gửi đi.
            HttpContent contentToSend = new StringContent(userJson,
                Encoding.UTF8, CONTENT_TYPE);
            // gọi shipper
            HttpClient httpClient = new HttpClient();
            // gửi đến đây (link), món quà này (contentToSend), chờ quá trình gửi thành công, thì lấy xác nhận từ người nhận.
            var response = await httpClient.PostAsync(REGISTER_API_URL, contentToSend);
            // đọc dữ liệu response từ người nhận.
            var stringContent = await response.Content.ReadAsStringAsync();
            // chuyển định dạng dữ liệu về đối tượng của C#
            var returnStudent = JsonConvert.DeserializeObject<User>(stringContent);
            // in ra một thuộc tính của đối tượng đó.
            Debug.WriteLine(JObject.Parse(stringContent)["id"]);
            return returnStudent;
        }
    }
}
