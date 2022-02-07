using iPractice.ApiBase.Response;
using Microsoft.AspNetCore.Mvc;

namespace iPractice.ApiBase.ApiBase
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        protected Response<TBody> ProduceResponse<TBody>(TBody body)
        {
            return Response<TBody>.Success(body);
        }
    }
}