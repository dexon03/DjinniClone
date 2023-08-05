﻿using System.Net;

namespace Core.Exceptions;

public class ExceptionWithStatusCode : Exception
{
    public ExceptionWithStatusCode(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;   
    }
    public HttpStatusCode StatusCode { get; set; }
}