namespace Revenue_Recognition_System.Models;

public class Software
{
    public int IdSoftware { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Version { get; set; }
    public decimal Price { get; set; }
    public int IdCategory { get; set; }
    public Category Category { get; set; }
}