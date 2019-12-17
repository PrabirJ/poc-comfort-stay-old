namespace ComfortStay.Repository
{
    using System.Collections.Generic;

    /// <summary>
    /// Response message was catch in this section of class
    /// </summary>
    public class SearchResponseMessage
    {
        /// <summary>
        /// List of Search Details object as parameter
        /// </summary>
        public List<SearchDetails> SearchDetails { get; set; }

        /// <summary>
        /// constructor contains the List of search details
        /// </summary>
        public SearchResponseMessage()
        {
            SearchDetails = new List<SearchDetails>();
        }
    }

}
