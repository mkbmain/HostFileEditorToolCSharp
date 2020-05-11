using System.Linq;

namespace HostFileEditor.Utils
{
    public static class StringExtensions
    {
        public static string ReadStringToChar(this string es, char delim)
        {
            return es.Split(delim).FirstOrDefault() ?? "";
        }
    }
}