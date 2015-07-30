
namespace Xen.Helpers
{
    public static class StringExtensions
    {
        /// <summary>
        /// Scrub the given string to replace &nbsp
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Scrub(this string text)
        {
            return text.Replace("&nbsp;", "");
        }
    }
}
