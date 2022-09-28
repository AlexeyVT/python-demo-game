using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using PythonDemoGame.Api.WorldClasses;

namespace PythonDemoGame.Api.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    [Route("/")]
    public IActionResult Index() => Ok("Hello World!!!");

    [HttpGet]
    [Route("/api/new_game")]
    public IActionResult NewGame(int mapSize)
    {
        GameService.World = new World(mapSize);
        GameService.World.Initialize();
        return Ok();
    }

    [HttpGet]
    [Route("/api/set_direction")]
    public IActionResult SetDirection(Direction direction)
    {
        if (GameService.World == null || GameService.World.IsGameOver)
        {
            return BadRequest();
        }

        GameService.World.Python.SetDirection(direction);
        return Ok();
    }

    [HttpGet]
    [Route("/api/get_map")]
    public IActionResult GetMap()
    {
        if (GameService.World == null)
        {
            return BadRequest();
        }

        return Ok(GameService.World.GetCells());
    }

    [HttpGet]
    [Route("/api/is_gameover")]
    public IActionResult IsGameOver()
    {
        if (GameService.World == null)
        {
            return BadRequest();
        }

        return Ok(GameService.World.IsGameOver);
    }
}