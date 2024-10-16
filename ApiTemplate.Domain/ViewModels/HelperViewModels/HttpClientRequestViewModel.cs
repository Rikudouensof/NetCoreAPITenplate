using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Domain.ViewModels.HelperViewModels
{
    public class HttpClientRequestViewModel
    {

        public int HttpClientCallFomatId { get; set; }

        public int HttpClientRequestSendTypeId { get; set; }

        public int HttpClientRequestAcceptTypeId { get; set; }

        public List<HttpClientMultipartPair>? MultipartPair { get; set; }

        public List<HttpClientHeaderParameter>? Headers { get; set; }

        public string BaseUrl { get; set; }
       
        public string Request { get; set; }

        public string PathUrl { get; set; }

        public string? jwtToken { get; set; }

        public HttpClientBasicAuthViewModel? BasicAuthCredential { get; set; }
    }
}
