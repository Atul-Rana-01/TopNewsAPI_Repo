using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopNews.Core;
using TopNews.Core.Entity;

namespace TopNews.Services
{
    public class TopNewsService : ITopNewsService
    {

        ITopNews _context;
        public TopNewsService(ITopNews entity) 
        {
            _context = entity;
        }

        public  List<TopNewsModel> GetTopNews()
        {
            List<TopNewsModel> lstData = _context.getTopNews().Result;
            return lstData;
        }
    }
}
