using Revenue_Recognition_System.Models;
using Revenue_Recognition_System.Repositories;

namespace Revenue_Recognition_System_Test.TestObjects;

public class FakeSoftwareRepository : ISoftwareRepository
{
    private ICollection<Software> _softwares;
    private ICollection<Category> _categories;

    public FakeSoftwareRepository()
    {
        _categories = new List<Category>
        {
            new Category { IdCategory = 1, Name = "Category 1" },
            new Category { IdCategory = 2, Name = "Category 2" },
            new Category { IdCategory = 3, Name = "Category 3" }
        };
        
        
        _softwares = new List<Software>
        {
            new Software
            {
                IdSoftware = 1,
                Name = "Software A",
                Description = "Description for Software A",
                Version = "1.0",
                Price = 199.99m,
                IdCategory = _categories.ElementAt(0).IdCategory,
                Category = _categories.ElementAt(0),
                Contracts = new List<Contract>()
            },
            new Software
            {
                IdSoftware = 2,
                Name = "Software B",
                Description = "Description for Software B",
                Version = "2.1",
                Price = 299.99m,
                IdCategory = _categories.ElementAt(1).IdCategory,
                Category = _categories.ElementAt(1),
                Contracts = new List<Contract>()
            },
            new Software
            {
                IdSoftware = 3,
                Name = "Software C",
                Description = "Description for Software C",
                Version = "3.0",
                Price = 399.99m,
                IdCategory = _categories.ElementAt(2).IdCategory,
                Category = _categories.ElementAt(2),
                Contracts = new List<Contract>()
            }
        };
    }

    public async Task<Software?> GetSoftwareAsync(int id, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_softwares
            .FirstOrDefault(software => software.IdSoftware == id));
    }
}