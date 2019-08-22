using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WishList.Shared.Result;

namespace WishList.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult CreateErrorResponse(Result operationResult)
        {
            switch (operationResult.Type)
            {
                case OperationResultType.InternalServerError:
                    return StatusCode(StatusCodes.Status500InternalServerError, operationResult);

                case OperationResultType.Conflict:
                    return StatusCode(StatusCodes.Status409Conflict, operationResult);

                case OperationResultType.NotFound:
                    return StatusCode(StatusCodes.Status404NotFound, operationResult);

                case OperationResultType.BadRequest:
                    return StatusCode(StatusCodes.Status400BadRequest, operationResult);

                default:
                    throw new System.Exception("OperationResult type not indentified");
            }
        }

        public IActionResult CreateErrorResponse<T>(Result<T> operationResult)
        {
            switch (operationResult.Type)
            {
                case OperationResultType.InternalServerError:
                    return StatusCode(StatusCodes.Status500InternalServerError, operationResult);

                case OperationResultType.Conflict:
                    return StatusCode(StatusCodes.Status409Conflict, operationResult);

                case OperationResultType.NotFound:
                    return StatusCode(StatusCodes.Status404NotFound, operationResult);

                case OperationResultType.BadRequest:
                    return StatusCode(StatusCodes.Status400BadRequest, operationResult);

                default:
                    throw new System.Exception("OperationResult type not indentified");
            }
        }
    }
}