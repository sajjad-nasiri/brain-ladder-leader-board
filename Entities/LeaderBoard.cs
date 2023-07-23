using System.ComponentModel.DataAnnotations;

namespace BrainLadder.Entities;

public class LeaderBoard
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Stat { get; set; }

    [Required]
    public string Score { get; set; }
}