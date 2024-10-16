using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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

        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;


            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj, GetJsonSerializerOptions()));


        }

        // Convert a byte array to an Object
        public static T ByteArrayToObject<T>(byte[] arrBytes)
        {
            if (arrBytes == null || !arrBytes.Any())
                return default;

            return JsonSerializer.Deserialize<T>(arrBytes, GetJsonSerializerOptions());
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                WriteIndented = true,
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
        }

    }
}
