namespace Syncromatics.Clients.Metro.Api.Models
{
  /// <summary>
  /// Represents metadata associated with a response to a request
  /// </summary>
  public class Metadata
    {
        /// <summary>
        /// Description of the request
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// Number of records found pertaining to the request
        /// </summary>
        public int RecordsFound { get; set; }

        /// <summary>
        /// Time that the request was received
        /// </summary>
        public string TimeOfRequest { get; set; }
    }
}