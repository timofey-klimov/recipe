﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Contracts.Web;

namespace Recipes.Web.Controllers
{
    public abstract class ApplicationController : ControllerBase
    {
        protected IMediator Mediator;

        protected ApplicationController(IMediator mediator)
        {
            Mediator = mediator;
        }
        protected Response<T> Created<T>(T data)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            HttpContext.Response.ContentType = "application/json";

            return Response<T>.Create(data);
        }

        protected Response<T> Ok<T>(T data)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            HttpContext.Response.ContentType = "application/json";
            return Response<T>.Create(data);
        }

        protected T Pagination<T>(T data)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            HttpContext.Response.ContentType = "application/json";
            return data;
        }
    }
}
