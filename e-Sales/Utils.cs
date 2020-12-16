using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;



namespace e_Sales
{
    partial class Program
    {
        public static class Utils : UtilsBase2
        {
            private static readonly UTF8Encoding Utf8NoBom = new UTF8Encoding(false);

            private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None,
                Converters = new JsonConverter[] { new StringEnumConverter() }
            };
        }
    }
}

