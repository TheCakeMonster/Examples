using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Csla.Server;

namespace Interception.Web.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly IDataPortalServer _dataPortal;

		public IndexModel(ILogger<IndexModel> logger, IDataPortalServer dataPortal)
		{
			_logger = logger;
			_dataPortal = dataPortal;
		}

		public async Task OnGet()
		{
			Type objectType;
			object criteria;
			DataPortalContext context;

			objectType = typeof(string);
			criteria = new object();
			context = new DataPortalContext();

			await _dataPortal.Fetch(objectType, criteria, context, true);
		}
	}
}