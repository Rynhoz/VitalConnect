using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB_VitalConnectApi.Pages
{
    public class GestionModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public GestionModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
