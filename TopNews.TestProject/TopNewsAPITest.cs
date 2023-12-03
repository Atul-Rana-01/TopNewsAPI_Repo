using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using TopNews.Core;
using TopNews.Core.Entity;
using TopNews.Data;
using TopNews.Services;
using TopNewsAPI.Controllers;
using Xunit;

namespace TopNews.TestProject
{
    public class TopNewsAPITest
    {
        private readonly TopNewsController _controller;
        private readonly ITopNews _service;
        private readonly IMemoryCache _memoryCache;

        public TopNewsAPITest(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _service = new TopNewsDAL();
            _controller = new TopNewsController(_service, _memoryCache);
        }

        // Test case for check method returning data or not null
        [Fact]
        public void Get_WhenCalled_ReturnsItems_NotNull()
        {
            // Act
            var okResult = _controller.Get();
            // Assert
            var items = Assert.IsType<List<TopNewsModel>>(okResult);
            Assert.NotNull(items);
        }


        // Test case for check method returning data in range
        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get();
            // Assert
            var items = Assert.IsType<List<TopNewsModel>>(okResult);
            Assert.InRange(items.Count, 1, 200);

        }



    }
}