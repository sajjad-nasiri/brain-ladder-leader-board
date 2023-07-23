using System.Text;
using System.Text.Json;
using BrainLadder.Services.LeaderBoards.Models;
using BrainLadder.Entities;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace BrainLadder.Services.LeaderBoards;

public interface ILeaderBoardService
{
    IEnumerable<LeaderBoardOut> GetScoresByStat(string stat);

    void AddScore(LeaderBoardIn model);
}

public class LeaderBoardService : ILeaderBoardService
{
    private readonly IConnectionMultiplexer _redis;
    
    public LeaderBoardService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public IEnumerable<LeaderBoardOut> GetScoresByStat(string stat)
    {
        var db = _redis.GetDatabase();

        var completeSet = db.SetMembers("UsernameSet");

        if (completeSet != null)
        {
            return Array
                .ConvertAll(completeSet, val => JsonSerializer.Deserialize<LeaderBoardOut>(val))
                .Where(o => o.Stat == stat)
                .ToList()
                .OrderByDescending(o => Convert.ToInt32(o.Score))
                .Take(20);
        }
        
        return null;
    }

    public void AddScore(LeaderBoardIn model)
    {
        var newEntity = new Entities.LeaderBoard();
        newEntity.Username = model.Username;
        newEntity.Stat = model.Stat;
        newEntity.Score = model.Score;

        var db = _redis.GetDatabase();

        var listOfLeaderBoard = JsonSerializer.Serialize(newEntity);

        db.StringSet(newEntity.Username, listOfLeaderBoard);
        db.SetAdd("UsernameSet", listOfLeaderBoard);
    }
}