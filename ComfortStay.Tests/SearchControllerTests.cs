﻿
namespace ComfortStay.Tests
{
    using ComfortStay.Controllers;
    using ComfortStay.Models;
    using ComfortStay.Repository;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class SearchControllerTests
    {
        private IConfiguration GetMockConfiguration()
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            var config = new ConfigurationBuilder()
               .SetBasePath(projectPath)
               .AddJsonFile("appsettings.json")
               .Build();
            return config;
        }        
        
        /// <summary>
        /// This test method take invalid model and when Find Bargain Method Call the exception Comes up
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GivenInvalidModel_WhenFindBargainIsCalled_ThenExceptionIsBubbledUp()
        {
            var repo = new Mock<ComfortStay.Repository.IHotelRepository>();
            var log = new Mock<ComfortStay.Utility.ILoggerService>();
            var svc = new SearchController(repo.Object, log.Object, GetMockConfiguration());
            repo.Setup(data => data.FindBargain(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Throws<Exception>();
            var result = await svc.Index(null) as ViewResult;
            Assert.Null(result.ViewName);
        }

        /// <summary>
        /// This test method return view return when Find Bargain Method Call
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GivenTheRepositoryReturnsResponse_WhenFindBargainIsCalled_ThenExpectedViewIsReturned()
        {
            var repo = new Mock<ComfortStay.Repository.IHotelRepository>();
            var log = new Mock<ComfortStay.Utility.ILoggerService>();
            var svc = new SearchController(repo.Object, log.Object, GetMockConfiguration());
            var expectedResponse = new List<SearchAPIJsonObject> {new SearchAPIJsonObject
            {
                Hotel = new Hotel
                {
                    Name = "sampleHotel",
                },
                Rates = new System.Collections.Generic.List<Rate>()
                {
                    new Rate
                    {
                        BoardType = "sampleType",
                        RateType = "Nights",
                        Value = 5
                    }
                }
            } };
            repo.Setup(data => data.FindBargain(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(expectedResponse);
            var result = await svc.Index(new SearchModel
            {
                DestinationId = 123,
                Nights = 12
            }) as ViewResult;
            Assert.Equal("Index", result.ViewName);
        }
    }
}
