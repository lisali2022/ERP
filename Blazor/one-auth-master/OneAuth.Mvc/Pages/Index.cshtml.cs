using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OneAuth.Mvc.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;


    [BindProperty]public string? UserName { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(string? returnUrl="/")
    {
        var identity = new ClaimsIdentity(authenticationType:"cookie");

        identity.AddClaim(new(ClaimTypes.Name, UserName));

        await HttpContext.SignInAsync(new(identity));

        return Redirect(returnUrl);
    }
}
