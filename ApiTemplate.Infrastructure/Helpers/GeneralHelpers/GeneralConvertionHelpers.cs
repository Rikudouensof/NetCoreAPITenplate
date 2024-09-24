using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiTemplate.Infrastructure.Helpers.GeneralHelpers
{
    public static class GeneralConvertionHelpers
    {
        public static string DateTimeToString(DateTime input)
        {
            return input.ToString("HH:mm ddd dd/MM/yyyy");
        }

        public static byte[] stringToyByteArray(string inputString)
        {
            return Encoding.ASCII.GetBytes(inputString);
        }

        public static string byteArrayToString(byte[] inputByte)
        {
            return Encoding.ASCII.GetString(inputByte);
        }

    }
}
