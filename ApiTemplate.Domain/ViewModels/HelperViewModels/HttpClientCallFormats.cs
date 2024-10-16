using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Domain.ViewModels.HelperViewModels
{
    public class HttpClientCallFormats
    {

        public int applicationJson { get; set; } = 1;
        public int multipartFormData { get; set; } = 2;




        public List<HttpClientCallFormat> GetListOfFormats()
        {
            return new List<HttpClientCallFormat>()
      {
        new HttpClientCallFormat() {Id = 1,Name = "application/json"},
         new HttpClientCallFormat() {Id = 2,Name = "multipart/form-data"},

      };
        }



        public HttpClientCallFormat GetSingleHttpClientCallFormat(int Id)
        {
            return GetListOfFormats().First(m => m.Id.Equals(Id));
        }
    }
}
