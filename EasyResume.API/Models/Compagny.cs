namespace EasyResume.API.Models;

public class Compagny : Entity
{
    public static readonly Compagny Empty = new Compagny
    {
        Id = 0,
        Name = string.Empty
    };
    public string Name { get; set; }
}