using Data.Managers;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dicemo.Web.Models;

namespace Dicemo.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly PitchManager _pitchManager;
    private readonly PlayerManager _playerManager;
    private readonly MatchManager _matchManager;

    public HomeController(ILogger<HomeController> logger, PitchManager pitchManager, PlayerManager playerManager, MatchManager matchManager)
    {
        _logger = logger;
        _pitchManager = pitchManager;
        _playerManager = playerManager;
        _matchManager = matchManager;
    }

    public async Task<IActionResult> Index()
    {
        var pitches = await _pitchManager.GetAllPitchesAsync();
        var players = await _playerManager.GetAllPlayersAsync();
        var openMatches = await _matchManager.GetOpenMatchesAsync();

        ViewBag.PitchCount = pitches.Count;
        ViewBag.PlayerCount = players.Count;
        ViewBag.OpenMatchCount = openMatches.Count;

        return View();
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
