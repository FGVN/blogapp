﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using blogapp.Controllers;
using blogapp.Services;
using blogapp.Models;

namespace blogapp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public JsonLogedService LogedService;
    public LoginController loginController;
    public JsonArticleService ArticleService;
    public ArticleController articleController;
    public IEnumerable<Article> articles;

    public Loged currentUser;

    /// <summary>
    /// Configuring services and controllers
    /// </summary>
    public IndexModel(ILogger<IndexModel> logger,
                    JsonLogedService logedservice,
                    JsonArticleService articleservice)
    {
        _logger = logger;
        LogedService = logedservice;
        loginController = new LoginController(LogedService);
        ArticleService = articleservice;
        articleController = new ArticleController(ArticleService);
    }

    /// <summary>
    /// On page loading getting all articles to display
    /// </summary>
    public void OnGet() => articles = articleController.GetArticles().Reverse();
}
