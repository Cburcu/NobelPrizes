using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HwNobelPrizes.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HwNobelPrizes.Controllers
{
    [Route("api/[controller]")]
    public class LaureatesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LaureatesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private async Task<List<LaureateData>> GetLaureateData()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://api.nobelprize.org");
            var result = await client.GetAsync("/v1/laureate.json");
            result.EnsureSuccessStatusCode();

            var stringResult = await result.Content.ReadAsStringAsync();
            List<LaureateData> data = JsonConvert.DeserializeObject<List<LaureateData>>(stringResult);

            return data;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<List<LaureateData>>> Get()
        {
            var data = await GetLaureateData();
            return data;
        }

        // GET api/<controller>/firstname=Pieter&surname=Zeeman
        [HttpGet("firstname={firstname}&surname={surname}")]
        public async Task<List<LaureateData>> GetByName(string firstname, string surname)
        {
            var data = await GetLaureateData();
            var result = data.Where(q => q.Firstname.Contains(firstname) && q.Surname.Contains(surname)).ToList();
            return result;
        }

        // GET api/<controller>/Germany
        [HttpGet("{country}")]
        public async Task<List<LaureateData>> GetByCountry(string country)
        {
            var data = await GetLaureateData();
            var result = data.Where(q => q.BornCountry.Contains(country)).ToList();
            return result;
        }
    }
}
