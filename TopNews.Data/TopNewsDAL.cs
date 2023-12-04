using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core;
using TopNews.Core.Entity;

namespace TopNews.Data
{
    public class TopNewsDAL : ITopNews
    {
        string apiBaseIDUrl = "https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty";
        string apiBaseDataUrl = "https://hacker-news.firebaseio.com/v0/item/";
        List<TopNewsModel> lstNews = new List<TopNewsModel>();
        public TopNewsDAL() { }


        // Method for getting ID's of top news
        public async Task<List<TopNewsModel>> getTopNews()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string endpoint = apiBaseIDUrl;
                    using (var response = await client.GetAsync(endpoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            // Deserialize data as list and selecting only top 200 IDs
                            var listId = JsonConvert.DeserializeObject<List<int>>(response.Content.ReadAsStringAsync().Result).Take(200).ToList();

                            // Calling method for getting data of each ID
                            await GetAllData(listId);
                        }
                    }
                }
                return lstNews;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // Method for getting data of each ID
        public async Task GetAllData(List<int> lstID)
        {
            List<int> lstID1 = new List<int>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    foreach (int id in lstID)
                    {
                        StringBuilder endpoint = new StringBuilder(apiBaseDataUrl);
                        endpoint.Append(id.ToString() + ".json?print=pretty");
                        using (var response = await client.GetAsync(endpoint.ToString()))
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                //Deserialize data and Mapping data to model
                                TopNewsModel resObj = JsonConvert.DeserializeObject<TopNewsModel>(response.Content.ReadAsStringAsync().Result);

                                // Selecting only those object where model property 'type' equals 'story'
                                if (resObj.type == "story")
                                    lstNews.Add(resObj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
