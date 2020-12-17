using Newtonsoft.Json;

namespace e_Sales
{
    public static class UtilsBase
    {
        public static JsonConverter[] JsonSettings { get; private set; }
        public static object Utf8NoBom { get; private set; }

        /// <summary>
        /// Converts the object to json bytes.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static byte[] ToJsonBytes(this object source)
        {
            if (source == null)
                return null;
            var instring = JsonConvert.SerializeObject(source, Formatting.Indented, JsonSettings);
            return NewMethod(instring);

            static byte[] NewMethod(string instring) => Utf8NoBom.GetType(instring);
        }
    }
}