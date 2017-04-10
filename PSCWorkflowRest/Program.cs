using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace PSCWorkflowRest
{
    class Program
    {
        static void Main(string[] args)
        {
			var httpClient = new HttpClient();

			var url = @"https://pscgroup.workflowcloud.com/api/v1/workflow/published/1bcc27c6-915c-4e99-99e0-206f593d57bf/instances";

			var token = @"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJOV0MiLCJ3b3JrZmxvd0lkIjoiMWJjYzI3YzYtOTE1Yy00ZTk5LTk5ZTAtMjA2ZjU5M2Q1N2JmIiwidGVuYW50SWQiOiJwc2Nncm91cCIsImlhdCI6MTQ5MTc0ODk1MX0.moGL5MhMg9447CjL1TJpAtmIXytH7fBk1K8ZhlkSrFc";

			var urlWithToken = string.Format(@"{0}?token={1}",url, token);

			var request = new HttpRequestMessage(HttpMethod.Post, urlWithToken);

			request.Content = new StringContent(JsonConvert.SerializeObject(new RestRequest()
			{
				startData = new StartData()
				{
					se_email_subject1 = "This is a test subject",
					se_email_body1 = "This is a test body",
					se_email_from1 = "hello@psclistens.com",
					se_email_to1 = "hello@psclistens.com"
				},
				options = new Options()
				{
					callbackUrl = ""
				},
			}), Encoding.UTF8, "application/json");

			request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

			var responce = httpClient.SendAsync(request).Result;

			var resposeContent = responce.Content.ReadAsStringAsync().Result;

			Console.WriteLine(resposeContent);

        }
    }

	public class StartData
	{
		public string se_email_subject1 { get; set; }
		public string se_email_body1 { get; set; }
		public string se_email_from1 { get; set; }
		public string se_email_to1 { get; set; }
	}

	public class Options
	{
		public string callbackUrl { get; set; }
	}

	public class RestRequest
	{
		public StartData startData { get; set; }
		public Options options { get; set; }
	}
}
