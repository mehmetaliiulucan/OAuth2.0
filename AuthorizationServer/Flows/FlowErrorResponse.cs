using AuthorizationServer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace AuthorizationServer.Flows
{
    public class FlowErrorResponse
    {
        #region Error Response As Json Result
        public static JsonResult InvalidClient(string errorDesc = null)
        {
            return new JsonResult(new
            {
                error = "invalid_client",
                error_description = errorDesc
            })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        public static JsonResult UnauthorizedClient(string errorDesc = null)
        {
            return new JsonResult(new
            {
                error = "unauthorized_client",
                error_description = errorDesc
            })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        public static JsonResult InvalidRequest(string errorDesc = null)
        {
            return new JsonResult(new
            {
                error = "invalid_request",
                error_description = errorDesc
            })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        public static JsonResult InvalidGrant(string errorDesc = null)
        {
            return new JsonResult(new
            {
                error = "invalid_grant",
                error_description = errorDesc
            })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        public static JsonResult UnsupportedGrantType(string errorDesc = null)
        {
            return new JsonResult(new
            {
                error = "unsupported_grant_type",
                error_description = errorDesc
            })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        public static JsonResult InvalidScope(string errorDesc = null)
        {
            return new JsonResult(new
            {
                error = "invalid_scope",
                error_description = errorDesc
            })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        public static JsonResult AccessDenied(string errorDesc = null)
        {
            return new JsonResult(new
            {
                error = "access_denied",
                error_description = errorDesc
            })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        public static JsonResult UnsupportedResponseType(string errorDesc = null)
        {
            return new JsonResult(new
            {
                error = "unsupported_response_type",
                error_description = errorDesc
            })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        #endregion

        #region Error Response As QueryString
        public static string InvalidClientError(string redirectUrl, string errorDesc = null)
        {
            return ErrorResponse(redirectUrl, "invalid_client", errorDesc);
        }
        public static string UnauthorizedClient(string redirectUrl, string errorDesc = null)
        {
            return ErrorResponse(redirectUrl, "unauthorized_client", errorDesc);
        }
        public static string InvalidRequest(string redirectUrl, string errorDesc = null)
        {
            return ErrorResponse(redirectUrl, "invalid_request", errorDesc);
        }
        public static string InvalidGrant(string redirectUrl, string errorDesc = null)
        {
            return ErrorResponse(redirectUrl, "invalid_grant", errorDesc);
        }
        public static string UnsupportedGrantType(string redirectUrl, string errorDesc = null)
        {
            return ErrorResponse(redirectUrl, "unsupported_grant_type", errorDesc);
        }
        public static string InvalidScope(string redirectUrl, string errorDesc = null)
        {
            return ErrorResponse(redirectUrl, "invalid_scope", errorDesc);
        }
        public static string AccessDenied(string redirectUrl, string errorDesc = null)
        {
            return ErrorResponse(redirectUrl, "access_denied", errorDesc);
        }
        public static string UnsupportedResponseType(string redirectUrl, string errorDesc = null)
        {
            return ErrorResponse(redirectUrl, "unsupported_response_type", errorDesc);
        }
        private static string ErrorResponse(string redirectUrl, string code, string errorDesc)
        {
            return $"{redirectUrl}?error={code}&error_description={errorDesc}";
        }

        #endregion

    }
}
