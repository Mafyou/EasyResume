namespace EasyResume.API.Models;

public class Job : Entity
{
    public static readonly Job Empty = new Job
    {
        Id = 0,
        Title = string.Empty,
        StartDate = DateTime.MinValue,
        EndDate = DateTime.MinValue,
        Compagny = Compagny.Empty
    };
    public string? Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Compagny? Compagny { get; set; }
}