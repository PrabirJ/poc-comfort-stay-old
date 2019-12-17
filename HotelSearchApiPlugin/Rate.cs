namespace ComfortStay.Repository
{
    /// <summary>
    /// Listed the rate on the basis of ratetype,boardtype,value
    /// </summary>
    public class Rate
    {
        /// <summary>
        /// rate type are of two type per night or stay
        /// </summary>
        public string RateType { get; set; }

        /// <summary>
        /// board type was the final price allocated
        /// </summary>
        public string BoardType { get; set; }


        /// <summary>
        /// Value the price charge against the stay
        /// </summary>
        public double Value { get; set; }
    }
}
