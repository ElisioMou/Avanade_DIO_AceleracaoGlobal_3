namespace e_Sales
{
    public static class UtilsBase
    {

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
            return Utf8NoBom.GetBytes(instring);
        }
    }
}