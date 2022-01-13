using Csla.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace BlazorAuthentication.WebUI.Pages
{
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  [IgnoreAntiforgeryToken]
  public class UserTestModel : PageModel
  {
    public string? RequestId { get; set; }

    public string? UserName { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    private readonly ILogger<ErrorModel> _logger;
    private readonly IContextManager _contextManager;

    public UserTestModel(ILogger<ErrorModel> logger, IContextManager contextManager)
    {
      _logger = logger;
      _contextManager = contextManager;
    }

    public void OnGet()
    {
      UserName = _contextManager.GetUser()?.Identity?.Name;
      RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
  }
}