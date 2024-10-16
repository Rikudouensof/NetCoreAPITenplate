using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Domain.ViewModels.HelperViewModels
{
    public class HttpClientRequestTypes
    {


        public int Get { get; set; } = 1;
        public int Post { get; set; } = 2;

        public int Put { get; set; } = 3;

        public int Delete { get; set; } = 4;





        public List<HttpClientRequestType> GetListOfFormats()
        {
            return new List<HttpClientRequestType>()
      {
        new HttpClientRequestType() {Id = 1,Name = "Get"},
        new HttpClientRequestType() {Id = 2,Name = "Post"},
        new HttpClientRequestType() {Id = 3,Name = "Put"},
        new HttpClientRequestType() {Id = 4,Name = "Delete"},
        new HttpClientRequestType() {Id = 5,Name = "Connect"},
        new HttpClientRequestType() {Id = 6,Name = "Head"},
        new HttpClientRequestType() {Id = 7,Name = "Options"},
        new HttpClientRequestType() {Id = 8,Name = "Trace"},
      };
        }



        public HttpClientRequestType GetSingleHttpClientRequestType(int Id)
        {
            return GetListOfFormats().First(m => m.Id.Equals(Id));
        }





    }
}
