namespace HotelSearchApiPlugin_Test
{
    using ComfortStay.Repository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;  

    [TestClass]
    public class SearchAPITest
    {
        private ISearchAPI searchApiHandler;
        private readonly string authCode = "aWH1EX7ladA8C/oWJX5nVLoEa4XKz2a64yaWVvzioNYcEo8Le8caJw==";
        private readonly string apiUrl = "https://webbedsdevtest.azurewebsites.net/api/";
        private readonly string apiMethodName = "findBargain";

        public SearchAPITest()
        {
            searchApiHandler = new SearchAPIImplementation(apiUrl);
        }

        [TestMethod]
        public async void OnlyAPICall_Test()
        {
            try
            {
                var destinationId = 279;
                var nightCount = 2;
                await searchApiHandler.FindBargain(apiMethodName, destinationId, nightCount, authCode);
            }
            catch (System.Exception)
            {
                Assert.Fail("API call returned error");
            }
        }

        [TestMethod]
        public async void RateType_PerNight_Test()
        {
            try
            {
                var destinationId = 279;
                var nightCount = 2;
                var result = await searchApiHandler.FindBargain(apiMethodName, destinationId, nightCount, authCode);
                if (result.SearchDetails != null)
                {
                    Assert.Fail("The Final Price calculated wrongly");
                }
            }
            catch (System.Exception)
            {
                Assert.Fail("API call returned error");
            }
        }
    }
}
