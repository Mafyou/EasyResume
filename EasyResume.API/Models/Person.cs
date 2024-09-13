namespace EasyResume.API.Models;

public class Person : Entity
{
    public static readonly Person Empty = new Person
    {
        Id = 0,
        Name = string.Empty,
        Surname = string.Empty,
        BirthDate = DateTime.MinValue,
        Jobs = []
    };
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public List<Job> Jobs { get; set; }
}