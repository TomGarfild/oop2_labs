namespace Kernel.Client.Options;

public class ApiOptions
{
    public string ApiUrl { get; set; }
    public string Url { get; set; }
    public Dictionary<string, string> Headers { get; set; }
}