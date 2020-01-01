﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UWP_API.Services
{

    class AuthenticationService
    {
        private static string CONTENT_TYPE = "application/json";
        private static string LOGIN_API_URL = "https://2-dot-backup-server-002.appspot.com/_api/v2/members/authentication";

        public async Task<String> GetToken(String email, String password)
        {
            JObject loginInfo = new JObject();
            loginInfo["email"] = email;
            loginInfo["password"] = password;
            //Chuyen thanh dang JSON
            var loginJson = JsonConvert.SerializeObject(loginInfo);
            //Dong goi gui len
            HttpContent contentToSend = new StringContent(loginJson,Encoding.UTF8,CONTENT_TYPE);
            //Goi shipper
            HttpClient httpClient = new HttpClient();
            //Lay ket qua
            var response = await httpClient.PostAsync(LOGIN_API_URL, contentToSend);
            //Doc ket qua
            var stringcontent = await response.Content.ReadAsStringAsync();
            return (string) JObject.Parse(stringcontent)["token"];
            
        }
    }
}