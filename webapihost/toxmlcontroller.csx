using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ToXmlController : System.Web.Http.ApiController 
{
	[HttpPost]
	public HttpResponseMessage Post(HttpRequestMessage request) 
	{
		string j = request.Content.ReadAsStringAsync().Result;
		JsonSerializer ser = new JsonSerializer();
		var jObj = ser.Deserialize(new JReader(new StringReader(j))) as JObject;
		var newJson = jObj.ToString(Newtonsoft.Json.Formatting.None);
		var xml = JsonConvert.DeserializeXmlNode(newJson,"result").OuterXml;
		
		return new HttpResponseMessage 
		{
			Content = new StringContent(xml, System.Text.Encoding.UTF8, "application/xml")
		};
	}
}

public class JReader : Newtonsoft.Json.JsonTextReader
{
    public JReader(TextReader r) : base(r)
    {
    }

    public override bool Read()
    {
        bool b = base.Read();
        if (base.CurrentState == State.Property && ((string)base.Value).Contains('/'))
        {
            base.SetToken(JsonToken.PropertyName,((string)base.Value).Replace("/", "_"));
        }
        return b;
    }
}