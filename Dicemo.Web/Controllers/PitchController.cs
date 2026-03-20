using Data.Enums;
using Data.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Dicemo.Web.Controllers;

public class PitchController : Controller
{
    private readonly PitchManager _pitchManager;

    public PitchController(PitchManager pitchManager)
    {
        _pitchManager = pitchManager;
    }

    public async Task<IActionResult> Index(string? q, SurfaceEnum? surface, PitchSizeEnum? size)
    {
        var pitches = await _pitchManager.GetAllPitchesAsync();

        if (!string.IsNullOrWhiteSpace(q))
            pitches = pitches.Where(p =>
                p.Name.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                p.Address.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList();

        if (surface.HasValue)
            pitches = pitches.Where(p => p.Surface == surface.Value).ToList();

        if (size.HasValue)
            pitches = pitches.Where(p => p.Size == size.Value).ToList();

        ViewBag.Q = q;
        ViewBag.Surface = surface;
        ViewBag.Size = size;

        return View(pitches);
    }
}
