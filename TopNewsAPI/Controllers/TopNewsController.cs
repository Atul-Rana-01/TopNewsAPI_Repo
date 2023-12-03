using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopNews.Core;
using TopNews.Core.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TopNewsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TopNewsController : ControllerBase
    {


        private readonly IMemoryCache _memoryCache;
        private readonly ITopNews _topnews;

        public TopNewsController(ITopNews topnews, IMemoryCache memoryCache)
        {
            this._topnews = topnews;
            this._memoryCache = memoryCache;
        }
        // GET: TopNewsController
        [HttpGet]
        public List<TopNewsModel> Get()
        {
            var lstTopNews = _memoryCache.Get<List<TopNewsModel>>("newsList");
            if (lstTopNews != null)
            {
                return lstTopNews;
            }
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            lstTopNews = _topnews.getTopNews().Result;
            _memoryCache.Set("newsList", lstTopNews, expirationTime);

            //  List<TopNewsModel> lstTopNews = _topnews.getTopNews().Result;
            return lstTopNews;
        }


    }
}

