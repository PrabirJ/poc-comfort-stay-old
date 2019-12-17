namespace Comfortstay.BusinessServices
{
    using ComfortStay.Repository;
    using Microsoft.Extensions.Configuration;
    using System.Threading.Tasks;

    /// <summary>
    /// This class basically is the srvice layer, will take data from repository and will send back to controller.
    /// </summary>
    public class HotelService
    {
        HotelRepository objHotelRepository = new HotelRepository();
        /// <summary>
        /// This method call hotel repository and send back the hotel detail to controller
        /// </summary>
        /// <param name="apiUrl" >url of api</param>
        /// <param name="apiMethodName">api method name</param>
        /// <param name="destinationId">destination id</param>
        /// <param name="nights">no of night </param>
        /// <param name="authCode">auth code</param>
        /// <returns></returns>
        public async Task<SearchResponseMessage> GetSearchDetail(string apiUrl, string apiMethodName, int destinationId, int nights, string authCode)
        {
            var result = await objHotelRepository.FindBargain(apiUrl, apiMethodName, destinationId, nights, authCode);
            SearchResponseMessage responseMessage = new SearchResponseMessage();
            if (result != null && result.Count > 0)
            {
                foreach (var hotel in result)
                {
                    foreach (var rate in hotel.Rates)
                    {
                        responseMessage.SearchDetails.Add(new SearchDetails
                        {
                            BoardType = rate.BoardType,
                            HotelName = hotel.Hotel.Name,
                            BasePrice = rate.Value,
                            ActualPrice = rate.RateType == "PerNight" ? rate.Value * nights : rate.Value,
                            RateType = rate.RateType
                        });
                    }
                }
            }

            return responseMessage;
        }
    }
}
