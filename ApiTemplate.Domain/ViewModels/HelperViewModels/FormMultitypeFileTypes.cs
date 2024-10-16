using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Domain.ViewModels.HelperViewModels
{
    public class FormMultitypeFileTypes
    {


        public int StringTypeId { get; private set; }

        public int ByteArrayTypeId { get; private set; }



        public FormMultitypeFileTypes()
        {
            Build();
        }

        private void Build()
        {
            var stringFileType = new FormMultitypeFileType()
            {
                Id = 1,
                Name = "StringType",


            };
            StringTypeId = stringFileType.Id;





            var byteArrayFaileType = new FormMultitypeFileType()
            {
                Id = 2,
                Name = "ByteArrayType",

            };
            ByteArrayTypeId = byteArrayFaileType.Id;

        }


    }
}
