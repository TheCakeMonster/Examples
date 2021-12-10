using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlazorExample.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DataPortalController : Csla.Server.Hosts.HttpPortalController
	{
		public DataPortalController(Csla.ApplicationContext applicationContext)
		  : base(applicationContext)
		{
			UseTextSerialization = true;
		}

		[HttpGet]
		public string Get()
		{
			return "Running";
		}

		public override async Task<Task> PostAsync([FromQuery] string operation)
		{
			await ReadBody();
			var result = base.PostAsync(operation);
			return result;
		}

		private async Task ReadBody()
		{
			try
			{
				string requestString;

				requestString = await GetRawBodyAsync(Request);

				Debug.WriteLine(requestString);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		private static async Task<string> GetRawBodyAsync(
			HttpRequest request,
			Encoding? encoding = null)
		{
			if (!request.Body.CanSeek)
			{
				// We only do this if the stream isn't *already* seekable,
				// as EnableBuffering will create a new stream instance
				// each time it's called
				request.EnableBuffering();
			}

			request.Body.Position = 0;

			var reader = new StreamReader(request.Body, encoding ?? Encoding.UTF8);

			var body = await reader.ReadToEndAsync().ConfigureAwait(false);

			request.Body.Position = 0;

			return body;
		}
	}
}