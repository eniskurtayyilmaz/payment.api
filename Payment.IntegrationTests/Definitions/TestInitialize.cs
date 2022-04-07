﻿using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Payment.Api;

namespace Payment.IntegrationTests.Definitions
{
    public abstract class TestInitialize
    {
        public HttpClient Client { get; set; }
        //public string ApiUri { get; set; }
        public TestInitialize()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            Client = appFactory.CreateClient();
            //ApiUri = Client?.BaseAddress?.AbsoluteUri;
        }

        public StringContent JsonData<T>(T data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public T ObjectData<T>(string json)
        {
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
    }
}