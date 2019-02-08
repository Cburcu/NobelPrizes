using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HwNobelPrizes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;

namespace HwNobelPrizes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrizesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PrizesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private async Task<PrizeList> GetPrizeData()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://api.nobelprize.org");
            var result = await client.GetAsync("/v1/prize.json");
            result.EnsureSuccessStatusCode();

            var stringResult = await result.Content.ReadAsStringAsync();
            PrizeList data = JsonConvert.DeserializeObject<PrizeList>(stringResult);

            return data;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<PrizeList>> Get()
        {
            var data = await GetPrizeData();
            return data;
        }

        // GET api/<controller>/getbycategory=physics
        [HttpGet("getbycategory={category}")]
        public async Task<List<Prizes>> GetByCategory(string category)
        {
            var data = await GetPrizeData();
            var resultByCategory = data.Prizes.Where(q => q.Category.Contains(category)).ToList();
            return resultByCategory;
        }
        // GET api/<controller>/getbyyear=2018
        [HttpGet("getbyyear={year}")]
        public async Task<List<Prizes>> GetByYear(string year)
        {
            var data = await GetPrizeData();
            var resultByYear = data.Prizes.Where(q => q.Year.Equals(year)).ToList();
            return resultByYear;
        }
        // GET api/<controller>/physics/2018
        [HttpGet("{category}/{year}")]
        public async Task<List<Prizes>> GetByCategoryAndYear(string category, string year)
        {
            var data = await GetPrizeData();
            var result = data.Prizes.Where(q => q.Category.Equals(category) && q.Year.Equals(year)).ToList();
            return result;
        }
        // GET api/<controller>/getbyname=Marie/Curie
        [HttpGet("getbyname={firstname}/{surname}")]
        public async Task<List<Prizes>> GetByName(string firstname, string surname)
        {
            var data = await GetPrizeData();
            var result = data.Prizes;
            List<Prizes> prizes = new List<Prizes>();
            foreach (var x in result)
            {
                List<Laureates> laureate = x.Laureates.Where(q => q.Firstname.Contains(firstname) && q.Surname.Contains(surname)).ToList();
                if (laureate.Count > 0)
                {
                    prizes.Add(x);
                }
            }
            return prizes;
        }
    }
}
