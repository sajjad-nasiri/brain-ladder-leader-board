using System.ComponentModel.DataAnnotations;

namespace BrainLadder.Services.LeaderBoards.Models;

public abstract class LeaderBoardBase
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Stat { get; set; }

    [Required]
    public string Score { get; set; }
}

public class LeaderBoardIn : LeaderBoardBase
{
}
	
public class LeaderBoardOut : LeaderBoardBase
{
}