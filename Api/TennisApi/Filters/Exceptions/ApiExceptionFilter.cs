using TennisApi.Shared.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisApi.Filters.Exceptions
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ApiResponse apiResponse = null;
            if (context.Exception is ApiException)
            {
                // handle explicit 'known' API errors
                var ex = context.Exception as ApiException;
                context.Exception = null;
                apiResponse = new ApiResponse(ex.Message, true);
                //apiError.ErrorList = ex.ErrorList;

                context.HttpContext.Response.StatusCode = ex.StatusCode;
            }
            else if(context.Exception is MissingFieldException)
            {
                var msg = context.Exception.GetBaseException().Message;
                apiResponse = new ApiResponse(msg, true);
                apiResponse.ErrorList = new List<string>();
                foreach (DictionaryEntry de in context.Exception.Data)
                {
                    apiResponse.ErrorList.Add(de.Value.ToString());
                }

                context.HttpContext.Response.StatusCode = 500;

            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                apiResponse = new ApiResponse("Unauthorized Access", true);
                context.HttpContext.Response.StatusCode = 401;

                // handle logging here
            }
            else
            {
                var msg = context.Exception.GetBaseException().Message;
                string detail = context.Exception.InnerException != null ? context.Exception.InnerException.GetBaseException().Message : "";

                apiResponse = new ApiResponse(msg, true);
                apiResponse.Detail = detail;

                context.HttpContext.Response.StatusCode = 500;

                // handle logging here
            }

            // always return a JSON result
            context.Result = new JsonResult(apiResponse);

            base.OnException(context);
        }
    }
}
