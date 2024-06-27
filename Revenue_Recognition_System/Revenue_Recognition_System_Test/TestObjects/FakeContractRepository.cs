using Revenue_Recognition_System.Models;
using Revenue_Recognition_System.Repositories;

namespace Revenue_Recognition_System_Test.TestObjects;

public class FakeContractRepository : IContractRepository
{
    private ICollection<Contract> _contracts;
    private ICollection<Payment> _payments;

    public FakeContractRepository()
    {
        _contracts = new List<Contract>
        {
            new Contract(new DateOnly(2024, 7, 12), 1, 3, 1, 1, 15000m){IdContract = 1},
            new Contract(new DateOnly(2024, 7, 1), null, 2, 1, 2, 20000m){IdContract = 2},
            new Contract(new DateOnly(2024, 7, 21), 2, 1, 1, 3, 25000m){IdContract = 3},
            new Contract(new DateOnly(2024, 6, 21), 2, 1, 2, 3, 25000m){IdContract = 4, StartDate = new DateOnly(2024, 6,1)}
        };

        _payments = new List<Payment>
        {
            new Payment(5000m, _contracts.ElementAt(0).IdContract, _contracts.ElementAt(0).IdClient){IdPayment = 1},
            new Payment(10000m, _contracts.ElementAt(1).IdContract, _contracts.ElementAt(1).IdClient){IdPayment = 2},
            new Payment(15000m, _contracts.ElementAt(2).IdContract, _contracts.ElementAt(2).IdClient){IdPayment = 3}
        };

        var naturalPersons = new List<NaturalPerson>()
        {
            new NaturalPerson("12345678901")
            {
                IdClient = 3,
                Name = "John",
                Surname = "Doe",
                Address = "123 Main St",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890"
            },
            new NaturalPerson("09876543210")
            {
                IdClient = 4,
                Name = "Jane",
                Surname = "Smith",
                Address = "456 Elm St",
                Email = "jane.smith@example.com",
                PhoneNumber = "098-765-4321"
            }
        };
        var companies = new List<Company>()
        {
            new Company("1234567890")
            {
                IdClient = 1,
                CompanyName = "Company One",
                Address = "123 Main St",
                Email = "info@companyone.com",
                PhoneNumber = "123-456-7890"
            },
            new Company("0987654321")
            {
                IdClient = 2,
                CompanyName = "Company Two",
                Address = "456 Elm St",
                Email = "info@companytwo.com",
                PhoneNumber = "098-765-4321"
            }
        };

        var discountTypes = new List<DiscountType>
        {
            new DiscountType
            {
                IdDiscountType = 1, Name = "Discount for one time purchase"
            },
            new DiscountType
            {
                IdDiscountType = 2, Name = "Discount for subscription"
            }
        };


        var categories = new List<Category>
        {
            new Category { IdCategory = 1, Name = "Category 1" },
            new Category { IdCategory = 2, Name = "Category 2" },
            new Category { IdCategory = 3, Name = "Category 3" }
        };


        var softwares = new List<Software>
        {
            new Software
            {
                IdSoftware = 1,
                Name = "Software A",
                Description = "Description for Software A",
                Version = "1.0",
                Price = 199.99m,
                IdCategory = categories[0].IdCategory,
                Category = categories[0],
                Contracts = new List<Contract>()
            },
            new Software
            {
                IdSoftware = 2,
                Name = "Software B",
                Description = "Description for Software B",
                Version = "2.1",
                Price = 299.99m,
                IdCategory = categories[1].IdCategory,
                Category = categories[1],
                Contracts = new List<Contract>()
            },
            new Software
            {
                IdSoftware = 3,
                Name = "Software C",
                Description = "Description for Software C",
                Version = "3.0",
                Price = 399.99m,
                IdCategory = categories[2].IdCategory,
                Category = categories[2],
                Contracts = new List<Contract>()
            }
        };

        var discounts = new List<Discount>
        {
            new Discount
            {
                IdDiscount = 1,
                Name = "Spring Sale",
                IdDiscountType = discountTypes[0].IdDiscountType,
                Offer = discountTypes[0],
                Value = 0.10m, // 10% discount
                DateFrom = new DateOnly(2024, 3, 1),
                DateTo = new DateOnly(2024, 3, 31),
                Contracts = new List<Contract>()
            },
            new Discount
            {
                IdDiscount = 2,
                Name = "Black Friday",
                IdDiscountType = discountTypes[1].IdDiscountType,
                Offer = discountTypes[1],
                Value = 0.50m, // $50 discount
                DateFrom = new DateOnly(2024, 11, 29),
                DateTo = new DateOnly(2024, 11, 29),
                Contracts = new List<Contract>()
            },
            new Discount
            {
                IdDiscount = 3,
                Name = "Cyber Monday",
                IdDiscountType = discountTypes[0].IdDiscountType,
                Offer = discountTypes[0],
                Value = 0.15m, // 15% discount
                DateFrom = new DateOnly(2024, 12, 2),
                DateTo = new DateOnly(2024, 12, 2),
                Contracts = new List<Contract>()
            }
        };

        _contracts.ElementAt(0).Paid = 15000m;
        _contracts.ElementAt(0).Payments = new List<Payment> { _payments.ElementAt(0) };

        _contracts.ElementAt(1).Paid = _payments.ElementAt(1).Value;
        _contracts.ElementAt(1).Payments = new List<Payment> { _payments.ElementAt(1) };

        _contracts.ElementAt(2).Paid = _payments.ElementAt(2).Value;
        _contracts.ElementAt(2).Payments = new List<Payment> { _payments.ElementAt(2) };

        discounts[0].Contracts.Add(_contracts.ElementAt(0));
        discounts[1].Contracts.Add(_contracts.ElementAt(2));

        softwares[0].Contracts.Add(_contracts.ElementAt(0));
        softwares[1].Contracts.Add(_contracts.ElementAt(1));
        softwares[2].Contracts.Add(_contracts.ElementAt(2));

        _contracts.ElementAt(0).Software = softwares[0];
        _contracts.ElementAt(0).Client = companies[0];
        _contracts.ElementAt(1).Software = softwares[0];
        _contracts.ElementAt(1).Client = companies[1];
        _contracts.ElementAt(2).Software = softwares[0];
        _contracts.ElementAt(2).Client = naturalPersons[1];
        _contracts.ElementAt(3).Software = softwares[1];
        _contracts.ElementAt(3).Client = naturalPersons[1];

        companies[1].Contracts = new List<Contract> { _contracts.ElementAt(1) };
        naturalPersons[0].Contracts = new List<Contract> { _contracts.ElementAt(2) };
        companies[0].Contracts = new List<Contract> { _contracts.ElementAt(0) };
    }

    public async Task<bool> IsPrevClientAsync(int contractIdClient, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_contracts.Any(contract => contract.IdClient == contractIdClient));
    }

    public async Task<bool> ClientHasContractForSoftwareAsync(int? boughtSoftware, int? client, CancellationToken cancellationToken)
    {
        //Assuming offer has 4 states (active, inactive, paid, finished) and is active only for time it can be paid for,
        //becoming paid if whole sum was paid in time and inactive is not
        return await Task.FromResult(_contracts
            .Any(c =>
                c.IdClient == client
                && c.IdSoftware == boughtSoftware
                && c.StartDate <= DateOnly.FromDateTime(DateTime.Now)
                && DateOnly.FromDateTime(DateTime.Now) <= c.EndDate
                && c.Paid < c.Value));
    }

    public async Task CreateContractAsync(Contract newContract, CancellationToken cancellationToken)
    {
        _contracts.Add(newContract);
    }

    public async Task<int> GetLastCreatedIdContractAsync(CancellationToken cancellationToken)
    {
        return await Task.FromResult(_contracts
            .OrderByDescending(c => c.IdContract)
            .Select(c => c.IdContract)
            .FirstOrDefault());
    }

    public async Task<Contract?> GetContractAsync(int id, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_contracts
            .FirstOrDefault(c => c.IdContract == id));
    }

    public async Task PayForContractAsync(Payment newPayment, CancellationToken cancellationToken)
    {
        _payments.Add(newPayment);
    }

    public void UpdateContractPaid(Contract contract, decimal paymentValue)
    {
        contract.Paid += paymentValue;
    }

    public async Task<int> GetLastPaymentIdAsync(CancellationToken cancellationToken)
    {
        return await Task.FromResult(_payments
            .OrderByDescending(c => c.IdPayment)
            .Select(c => c.IdPayment)
            .FirstOrDefault());
    }

    public async Task<Payment?> GetPaymentAsync(int id, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_payments
            .FirstOrDefault(p => p.IdPayment == id));
    }

    public async Task<decimal> GetRevenueAsync(CancellationToken cancellationToken)
    {
        return await Task.FromResult(_contracts
            .Where(c => c.Paid == c.Value)
            .Sum(c => c.Value));
    }

    public async Task<decimal> GetRevenueAsync(int idSoftware, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_contracts
            .Where(c => c.Paid == c.Value)
            .Where(c => c.Software.IdSoftware == idSoftware)
            .Sum(c => c.Value));
    }

    public async Task<decimal> GetExpectedRevenue(CancellationToken cancellationToken)
    {
        return await Task.FromResult(_contracts
            .Sum(c => c.Value));
    }
}