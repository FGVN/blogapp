using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace blogapp.Pages;

public class Signup : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public Signup(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

