namespace AuthorizationServer.Extension
{
    public static class StringUtil
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
