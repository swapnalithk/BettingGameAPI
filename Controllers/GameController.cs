using BettingGameApi.Services;
using BettingGameAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BettingGameApi.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public string Get()
        {
            return "Application Running Successfully";
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Player playerRequest)
        {
            var player = await _gameService.RegisterPlayer(playerRequest.Username);
            if (player == null)
                return BadRequest(new { message = "Player already exists." });

            return Ok(new { player.Username, player.AccountBalance });
        }

        [HttpGet("balance/{username}")]
        public async Task<IActionResult> GetBalance(string username)
        {
            var player = await _gameService.GetPlayerBalance(username);
            if (player == null)
                return NotFound(new { message = "Player not found." });

            return Ok(new { player.Username, player.AccountBalance });
        }

        [HttpPost("bet/{playerId}")]
        public async Task<IActionResult> PlaceBet(int playerId, [FromBody] BetRequest bet)
        {
            var (success, message, result) = await _gameService.PlaceBet(playerId, bet.Points, bet.Number);
            if (!success)
                return BadRequest(new { message });

            return Ok(result);
        }

        [HttpGet("bet-history/{playerId}")]
        public async Task<IActionResult> GetBetHistory(int playerId)
        {
            var bets = await _gameService.GetBetHistory(playerId);
            if (bets == null || bets.Count == 0)
                return NotFound(new { message = "No bet history found for this player." });

            return Ok(bets);
        }
    }
}
