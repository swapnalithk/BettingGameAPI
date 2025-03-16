using BettingGameAPI.Data;
using BettingGameAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BettingGameApi.Services
{
    public class GameService
    {
        private readonly GameDbContext _context;
        private readonly Random _random;

        public GameService(GameDbContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task<Player?> RegisterPlayer(string username)
        {
            var existingPlayer = await _context.Players.FirstOrDefaultAsync(p => p.Username == username);
            if (existingPlayer != null)
                return null; 

            var player = new Player { Username = username, AccountBalance = 10000 };
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<Player?> GetPlayerBalance(string username)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.Username == username);
        }

        public async Task<(bool success, string message, object? result)> PlaceBet(int playerId, int points, int predictedNumber)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);
            if (player == null)
                return (false, "Player not found.", null);

            if (points <= 0 || points > player.AccountBalance)
                return (false, "Invalid bet amount.", null);

            if (predictedNumber < 0 || predictedNumber > 9)
                return (false, "Invalid number. The prediction must be between 0 and 9.", null);

            int generatedNumber = _random.Next(0, 10);
            bool isWinner = predictedNumber == generatedNumber;
            int winnings = isWinner ? points * 9 : -points;

            player.AccountBalance += winnings;

            var newBet = new Bet
            {
                PlayerId = playerId,
                Points = points,
                PredictedNumber = predictedNumber,
                GeneratedNumber = generatedNumber,
                IsWin = isWinner,
                Timestamp = DateTime.UtcNow
            };

            _context.Bets.Add(newBet);
            await _context.SaveChangesAsync();

            return (true, "Bet placed successfully.", new
            {
                account = player.AccountBalance,
                status = isWinner ? "won" : "lost",
                points = winnings,
                generatedNumber
            });
        }

        public async Task<List<Bet>> GetBetHistory(int playerId)
        {
            return await _context.Bets
                .Where(b => b.PlayerId == playerId)
                .Include(b => b.Player)
                .OrderByDescending(b => b.Timestamp)
                .ToListAsync();
        }
    }
}
