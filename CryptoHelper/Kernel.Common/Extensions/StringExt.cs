using Newtonsoft.Json;

namespace Kernel.Common.Extensions;

public static class StringExt
{
    public static string ToFormattedJson(this string str)
    {
        dynamic parsedJson = JsonConvert.DeserializeObject(str)!;
        return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
    }
}