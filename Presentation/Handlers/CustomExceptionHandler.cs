using Amaris.Models;
using BLL.Model.DTOs;
using DAL.Exceptions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Amaris.Handlers;

public class CustomExceptionHandler: ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        HttpResponseException responseException = new HttpResponseException();
        ResponseDto response = new ResponseDto();

        if (context.Exception is TooManyRequestException)
        {
            responseException.Status = StatusCodes.Status429TooManyRequests;
            response.Message = "You have reached the request limit. Please try again later";
            context.ExceptionHandled = true;
        }
        else
        {
            responseException.Status = StatusCodes.Status500InternalServerError;
            response.Message = "An internal error has occurred, please try again";
            context.ExceptionHandled = true;
        }

        context.Result = new ObjectResult(responseException.Value)
        {
            StatusCode = responseException.Status,
            Value = response
        };
        
        if (responseException.Status == StatusCodes.Status500InternalServerError)
            context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "An internal error has occurred, please try again";
    }
}