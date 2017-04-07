using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities
{
    /// <summary>
    /// Information used when calling an API which supports paging
    /// </summary>
    [DataContract]
    public class PagingInfo
    {
        /// <summary>
        /// Specifies the size of a page
        /// </summary>
        [DataMember(Name = "limit", EmitDefaultValue = false)]
        public int Limit { get; set; }

        /// <summary>
        /// This is returned by pagable services and if this is true, there are not more results available
        /// </summary>
        [DataMember(Name = "isLastPage", EmitDefaultValue = false)]
        public bool IsLastPage { get; set; }

        /// <summary>
        /// This is returned by pagable services and specifies the value for the start to get the next results.
        /// </summary>
        [DataMember(Name = "nextPageStart", EmitDefaultValue = false)]
        public int NextPageStart { get; set; }
    }
}
