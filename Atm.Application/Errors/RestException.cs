﻿namespace Atm.Application.Errors
{
    using System;
    using System.Net;

    public class RestException : Exception
    {
        public HttpStatusCode Code { get; }

        public object Errors { get; }

        public RestException(HttpStatusCode code, object errors = null)
        {
            this.Code = code;
            this.Errors = errors;
        }
    }
}
