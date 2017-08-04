namespace Syncromatics.Clients.Metro.Api.Models
{
  /// <summary>
  /// Common response format
  /// </summary>
  public abstract class BaseResponse
    {
        /// <summary>
        /// Metadata associated with the response
        /// </summary>
        public Metadata Metadata { get; set; }
    }
}