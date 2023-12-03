using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TopNews.Core.Entity
{
   public interface ITopNews 
    {
        Task<List<TopNewsModel>> getTopNews();
    }
}

//https://hacker-news.firebaseio.com/v0/item/8863.json?print=pretty

//https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty
