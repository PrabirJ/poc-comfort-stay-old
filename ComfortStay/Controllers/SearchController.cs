namespace ComfortStay.Controllers
{
    using Comfortstay.BusinessServices;
    using ComfortStay.Models;
    using ComfortStay.Repository;
    using ComfortStay.Utility;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Threading.Tasks;

    public class SearchController : Controller
    {

        private IHotelRepository searchHotel;
        private ILoggerService logToFile;
        private AppSettings appSettings;


    /// <summary>
    /// Constructor for dependency injection
    /// </summary>
    /// <param name="searchHotelAPI">Search hotel interface</param>
    /// <param name="logFile">Logger interface</param>
    /// <param name="config">Configuration interface</param>
        public SearchController(IHotelRepository searchHotelAPI, ILoggerService logFile, IConfiguration config)
        {
            this.searchHotel = searchHotelAPI;
            this.logToFile = logFile;
            appSettings = new AppSettings
            {
                ExtApiAuthCode = config.GetSection("AppSettings")["ExteralAPI_AuthCode"],
                ExtApiMethodName = config.GetSection("AppSettings")["ExternalAPI_MethodName"],
                ExtApiUrl = config.GetSection("AppSettings")["ExternalAPI_URL"]
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This controller method will call the service layer to get hotel details
        /// </summary>
        /// <param name="searchModel">Model data from View</param>
        /// <returns return="ActionResult"></returns>
        [HttpPost]
        public async Task<ActionResult> Index(SearchModel searchModel)
        {
            HotelService objHotelService = new HotelService();
            try
            {
                if (searchModel == null)
                {
                    return View(searchModel);
                }
                var authCode = appSettings.ExtApiAuthCode;
                var apiMethodName = appSettings.ExtApiMethodName;
                var apiUrl = appSettings.ExtApiUrl;

                ViewData["ResultSet"] = await objHotelService.GetSearchDetail(apiUrl, apiMethodName, searchModel.DestinationId, searchModel.Nights, authCode);

                return View("Index");
            }
            catch (Exception ex)
            {
                logToFile.LogToFile("C:\\Logging", "ExceptionLog", ex.Message);
                return View("../Views/Shared/Error.cshtml", new ErrorModel { RequestId = "123", ExceptionMessage = ex.Message });
            }
        }
    }
}