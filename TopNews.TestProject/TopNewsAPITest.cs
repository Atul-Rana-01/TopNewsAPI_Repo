using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using TopNewsAPI.Controllers;
using TopNews.Core.Entity;
using Xunit;
using TopNews.Core;

public class TopNewsControllerTests
{

    [Fact]
    public void GetData_ReturnsTopNewsList()
    {
        // Arrange
        var expectedNews = new List<TopNewsModel>
        {
            { new TopNewsModel { ID = 38516331, title = "Gobs of data (2011)", url = "https://go.dev/blog/gob", type = "story" } },
            { new TopNewsModel { ID = 38519012, title = "Harvard gutted team examining Facebook Files following $500M Zuckerberg donation", url = "https://live-whistleblower-aid.pantheonsite.io/joan-donovan-press-release/", type = "story" } },
            { new TopNewsModel { ID = 38519257, title = "A Decade of Have I Been Pwned", url = "https://www.troyhunt.com/a-decade-of-have-i-been-pwned/", type = "story" } },
            { new TopNewsModel { ID = 38513402, title = "A minimum complete tutorial of Linux ext4 file system (2017)", url = "https://metebalci.com/blog/a-minimum-complete-tutorial-of-linux-ext4-file-system/", type = "story" } },
            { new TopNewsModel { ID = 38517846, title = "Oxford Word of the Year", url = "https://languages.oup.com/word-of-the-year/2023/", type = "story" } }

        // Add more news items as needed
    };

        var topNewsServiceMock = new Mock<ITopNewsService>();
        topNewsServiceMock.Setup(service => service.GetTopNews()).Returns(expectedNews);
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var topNewsObj = new TopNewsController(topNewsServiceMock.Object, memoryCache);

        // Act
        var result = topNewsObj.getData();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedNews, result);
    }

    [Fact]
    public void Get_ReturnsCachedData_WhenAvailableInMemoryCache()
    {
        // Arrange
        var cacheKey = "newsList";
        var testNews = new List<TopNewsModel> 
        {   { new TopNewsModel { ID = 38516331, title = "Gobs of data (2011)", url = "https://go.dev/blog/gob", type = "story" } },
            { new TopNewsModel { ID = 38519012, title = "Harvard gutted team examining Facebook Files following $500M Zuckerberg donation", url = "https://live-whistleblower-aid.pantheonsite.io/joan-donovan-press-release/", type = "story" } },
            { new TopNewsModel { ID = 38519257, title = "A Decade of Have I Been Pwned", url = "https://www.troyhunt.com/a-decade-of-have-i-been-pwned/", type = "story" } },
            { new TopNewsModel { ID = 38513402, title = "A minimum complete tutorial of Linux ext4 file system (2017)", url = "https://metebalci.com/blog/a-minimum-complete-tutorial-of-linux-ext4-file-system/", type = "story" } },
            { new TopNewsModel { ID = 38517846, title = "Oxford Word of the Year", url = "https://languages.oup.com/word-of-the-year/2023/", type = "story" } }

        };

        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        memoryCache.Set(cacheKey, testNews);

        var topNewsServiceMock = new Mock<ITopNewsService>();

        var topNewsObj = new TopNewsController(topNewsServiceMock.Object, memoryCache);

        // Act
        var result = topNewsObj.Get();

        // Assert
        Assert.Equal(testNews, result);
        topNewsServiceMock.Verify(service => service.GetTopNews(), Times.Never);
    }

    [Fact]
    public void Get_ReturnsFreshData_WhenNotAvailableInMemoryCache()
    {
        // Arrange
        var cacheKey = "newsList";
        var testNews = new List<TopNewsModel> {
            { new TopNewsModel { ID = 38516331, title = "Gobs of data (2011)", url = "https://go.dev/blog/gob", type = "story" } },
            { new TopNewsModel { ID = 38519012, title = "Harvard gutted team examining Facebook Files following $500M Zuckerberg donation", url = "https://live-whistleblower-aid.pantheonsite.io/joan-donovan-press-release/", type = "story" } },
            { new TopNewsModel { ID = 38519257, title = "A Decade of Have I Been Pwned", url = "https://www.troyhunt.com/a-decade-of-have-i-been-pwned/", type = "story" } },
            { new TopNewsModel { ID = 38513402, title = "A minimum complete tutorial of Linux ext4 file system (2017)", url = "https://metebalci.com/blog/a-minimum-complete-tutorial-of-linux-ext4-file-system/", type = "story" } },
            { new TopNewsModel { ID = 38517846, title = "Oxford Word of the Year", url = "https://languages.oup.com/word-of-the-year/2023/", type = "story" } }

        };

        var memoryCache = new MemoryCache(new MemoryCacheOptions());

        var topNewsServiceMock = new Mock<ITopNewsService>();
        topNewsServiceMock.Setup(service => service.GetTopNews()).Returns(testNews);

        var topNewsObj = new TopNewsController(topNewsServiceMock.Object, memoryCache);

        // Act
        var result = topNewsObj.Get();

        // Assert
        Assert.Equal(testNews, result);
        topNewsServiceMock.Verify(service => service.GetTopNews(), Times.Once);
    }


}