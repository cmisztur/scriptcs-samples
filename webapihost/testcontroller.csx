public class TestController : System.Web.Http.ApiController {

	public string Get(string message) {
		return "Hello world!" + message;
	}
}