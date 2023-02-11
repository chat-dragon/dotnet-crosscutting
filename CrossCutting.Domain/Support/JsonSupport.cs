using System.Text;
using System.Text.Json;

namespace CrossCutting.Domain.Support
{
    public static class JsonSupport
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
