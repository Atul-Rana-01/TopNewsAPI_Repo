using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TopNews.Core.Entity
{
   public interface ITopNewsService
    {
        List<TopNewsModel> GetTopNews();
    }
}


