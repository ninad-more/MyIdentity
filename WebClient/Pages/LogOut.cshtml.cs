using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages
{
    public class LogOutModel : PageModel
    {        public IActionResult OnGet()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}
