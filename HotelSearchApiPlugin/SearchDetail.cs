namespace ComfortStay.Repository
{
    /// <summary>
    /// Search detail is the total classification of the hotel on the basis board type, hotel name,rate , base price,actual price
    /// </summary>
    public class SearchDetails
        {
            /// <summary>
            /// Broad type the final price associated 
            /// </summary>
            public string BoardType { get; set; }

            /// <summary>
            /// Name of the hotel
            /// </summary>
            public string HotelName { get; set; }

            /// <summary>
            /// Rate catagory
            /// </summary>
            public string RateType { get; set; }

            /// <summary>
            /// Base price
            /// </summary>
            public double BasePrice { get; set; }
            
            /// <summary>
            /// Actual price the proper price associated with hotel
            /// </summary>
            public double ActualPrice { get; set; }
        }

}
