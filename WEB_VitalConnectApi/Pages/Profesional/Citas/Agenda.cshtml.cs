using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB_VitalConnectApi.Pages
{
    public class AgendaModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public AgendaModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
