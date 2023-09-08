using System.Net;

namespace Public.DTO.v1;
/// <summary>
/// rest api error response
/// </summary>
public class RestApiErrorResponse
{
    public HttpStatusCode Status { get; set; }
    public string Error { get; set; } = default!;
}