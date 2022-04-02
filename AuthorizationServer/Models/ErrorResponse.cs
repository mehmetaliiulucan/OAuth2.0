using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace AuthorizationServer.Models
{
    public class ErrorResponse
    {
        [JsonProperty("error")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ErrorTypeEnum Error { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

        [JsonProperty("error_uri")]
        public string ErrorUri { get; set; }
    }

    public enum ErrorTypeEnum
    {
        [EnumMember(Value = "invalid_request")]
        InvalidRequest,

        [EnumMember(Value = "invalid_client")]
        InvalidClient,

        [EnumMember(Value = "invalid_grant")]
        InvalidGrant,

        [EnumMember(Value = "unauthorized_client")]
        UnauthorizedClient,

        [EnumMember(Value = "unsupported_grant_type")]
        UnsupportedGrantType,
        
        [EnumMember(Value = "invalid_scope")]
        InvalidScope,

        [EnumMember(Value = "access_denied")]
        AccessDenied,

        [EnumMember(Value = "server_error")]
        ServerError,

        [EnumMember(Value = "unsupported_response_type")]
        UnsupportedResponseType,
    }
}
