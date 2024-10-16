using ApiTemplate.Domain.ViewModels.HelperViewModels;
using ApiTemplate.Infrastructure.Helpers.GeneralHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Infrastructure.Helpers
{
    public class HttpClientHelper
    {


        readonly HttpClientCallFormats contentType = new HttpClientCallFormats();
        readonly HttpClientRequestTypes requestType = new HttpClientRequestTypes();
        readonly FormMultitypeFileTypes formFileTypes = new FormMultitypeFileTypes();




        public async Task<string> MakeHTTPRequest(HttpClientRequestViewModel input)
        {

            string response = "Error";
            var acceptRequestType = contentType.GetSingleHttpClientCallFormat(input.HttpClientRequestAcceptTypeId);
            var sendRequestType = contentType.GetSingleHttpClientCallFormat(input.HttpClientRequestSendTypeId);
            var currentRequestType = requestType.GetSingleHttpClientRequestType(input.HttpClientCallFomatId);

            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(input.BaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptRequestType.Name));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", sendRequestType.Name);

            if (input.Headers is not null)
            {
                foreach (var header in input.Headers)
                {
                    client.DefaultRequestHeaders.Add(header.Name, header.Value);
                }
            }
            if (input.BasicAuthCredential is not null)
            {
                var authenticationString = $"{input.BasicAuthCredential.Username}:{input.BasicAuthCredential.Password}";
                var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));

                // Set the Authorization header
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            }
            if (input.jwtToken is not null)
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + input.jwtToken);
            }


            if (input.HttpClientRequestSendTypeId == contentType.multipartFormData)
            {
                response = await MultipartData(client, input);

            }
            else if (input.HttpClientRequestSendTypeId == contentType.applicationJson)
            {
                response = await JsonRequest(client, input);
            }
            return response;
        }

        private async Task<string> JsonRequest(HttpClient client, HttpClientRequestViewModel input)
        {
            var getmethodName = nameof(JsonRequest);
            var currentRequestType = requestType.GetSingleHttpClientRequestType(input.HttpClientCallFomatId);
            string response = "";



            try
            {
                if (input.HttpClientCallFomatId == requestType.Get)
                {
                    var apiResponse = await client.GetAsync(input.PathUrl);
                    var apiResponseContent = await apiResponse.Content.ReadAsStringAsync();
                    response = apiResponseContent;
                }
                else if (input.HttpClientCallFomatId == requestType.Post)
                {
                    var stringContent = new StringContent(input.Request, Encoding.UTF8, "application/json");
                    var apiResponse = await client.PostAsync(input.PathUrl, stringContent);
                    var apiResponseContent = await apiResponse.Content.ReadAsStringAsync();
                    response = apiResponseContent;
                }



            }
            catch (Exception ex)
            {

            }
            return response;

        }

        private async Task<string> MultipartData(HttpClient client, HttpClientRequestViewModel input)
        {
            var getmethodName = nameof(JsonRequest);
            var currentRequestType = requestType.GetSingleHttpClientRequestType(input.HttpClientCallFomatId);
            string response = " ";
            using (var multipartFormDataContent = new MultipartFormDataContent())
            {
                try
                {
                    for (int counter = 0; counter < input.MultipartPair.Count; counter++)
                    {
                        var formData = input.MultipartPair[counter];
                        if (formData.FormContentTypeId == formFileTypes.StringTypeId)
                        {
                            multipartFormDataContent.Add(new StringContent(formData.FormContent.ToString()),
                               string.Format("\"{0}\"", formData.FormName));
                        }
                        else if (formData.FormContentTypeId == formFileTypes.ByteArrayTypeId && !string.IsNullOrEmpty(formData.FileName))
                        {
                            multipartFormDataContent.Add(new ByteArrayContent( GeneralConvertionHelpers.ObjectToByteArray(formData.FormContent)), formData.FileName);
                        }


                    }


                    if (input.HttpClientCallFomatId == requestType.Post)
                    {
                        var apiResponse = await client.PostAsync(input.PathUrl, multipartFormDataContent);
                        var apiResponseContent = await apiResponse.Content.ReadAsStringAsync();
                        response = apiResponseContent;
                    }

                }
                catch (Exception ex)
                {

                }





            }

            return response;
        }
    }
}
