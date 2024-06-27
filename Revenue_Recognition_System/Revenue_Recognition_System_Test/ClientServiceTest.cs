using Revenue_Recognition_System_Test.TestObjects;
using Revenue_Recognition_System.DTO;
using Revenue_Recognition_System.Enums;
using Revenue_Recognition_System.Exceptions;
using Revenue_Recognition_System.Repositories;
using Revenue_Recognition_System.Services;
using Shouldly;

namespace Revenue_Recognition_System_Test;

public class ClientServiceTest
{

    private readonly IClientService _service;

    public ClientServiceTest()
    {
        _service = new ClientService(new FakeClientRepository(), new FakeUnitOfWork());
    }

    [Fact]
    public async Task AddClientAsync_DataForNaturalPersonPropertiesNotGiven_ShouldThrowDomainException()
    {
        //Arrange
        var client = new ClientDTO("asfdasf", "fasdfa", "13122412", ClientType.NaturalPerson,
            new TypeSpecificPropertiesDTO(null, null, null, null, null));

        //Act and Assert
        await Should.ThrowAsync<DomainException>(_service.AddClientAsync(client, CancellationToken.None));
    }

    [Fact]
    public async Task AddClientAsync_DataForCompanyPropertiesNotGiven_ShouldThrowDomainException()
    {
        //Arrange
        var client = new ClientDTO("asfdasf", "fasdfa", "13122412", ClientType.Company,
            new TypeSpecificPropertiesDTO(null, null, null, null, null));

        //Act and Assert
        await Should.ThrowAsync<DomainException>(_service.AddClientAsync(client, CancellationToken.None));
    }

    [Fact]
    public async Task AddClientAsync_NaturalPersonWithPeselInDatabase_ShouldThrowDomainException()
    {
        //Arrange
        var client = new ClientDTO("asfdasf", "fasdfa", "13122412", ClientType.NaturalPerson,
            new TypeSpecificPropertiesDTO("adfas", "asdf", "12345678901", null, null));

        //Act and Assert
        await Should.ThrowAsync<DomainException>(_service.AddClientAsync(client, CancellationToken.None));
    }

    [Fact]
    public async Task AddClientAsync_CompanyWithKRSInDatabase_ShouldThrowDomainException()
    {
        //Arrange
        var client = new ClientDTO("asfdasf", "fasdfa", "13122412", ClientType.Company,
            new TypeSpecificPropertiesDTO(null, null, null, "Company One", "1234567890"));

        //Act and Assert
        await Should.ThrowAsync<DomainException>(_service.AddClientAsync(client, CancellationToken.None));
    }

    [Fact]
    public async Task AddClientAsync_NaturalPersonNotInDatabase_ShouldReturnClientViewDTO()
    {
        //Arrange
        var client = new ClientDTO("asfdasf", "fasdfa", "13122412", ClientType.NaturalPerson,
            new TypeSpecificPropertiesDTO("adfas", "asdf", "4412414124", null, null));

        //Act and Assert
        var result = _service.AddClientAsync(client, CancellationToken.None);
        await Should.NotThrowAsync(result);
    }

    [Fact]
    public async Task AddClientAsync_CompanyNotInDatabase_ShouldReturnClientViewDTO()
    {
        //Arrange
        var client = new ClientDTO("asfdasf", "fasdfa", "13122412", ClientType.Company,
            new TypeSpecificPropertiesDTO(null, null, null, "Company One", "1231244423"));

        //Act and Assert
        var result = _service.AddClientAsync(client, CancellationToken.None);
        await Should.NotThrowAsync(result);
    }

    [Fact]
    public async Task DeleteClientAsync_TryingToDeleteCompany_ShouldThrowDomainException()
    {
        //Act and Assert
        await Should.ThrowAsync<DomainException>(_service.DeleteClientAsync(1, new CancellationToken()));
    }

    [Fact]
    public async Task DeleteClientAsync_ClientNotInDatabase_ShouldThrowDomainException()
    {
        //Act and Assert
        await Should.ThrowAsync<DomainException>(_service.DeleteClientAsync(1231, new CancellationToken()));
    }

    [Fact]
    public async Task DeleteClientAsync_TryingDeleteNormalPerson_ShouldThrowDomainException()
    {
        //Act and Assert
        await Should.NotThrowAsync(_service.DeleteClientAsync(3, new CancellationToken()));
    }

    [Fact]
    public async Task UpdateClientAsync_DataForNaturalPersonPropertiesNotGiven_ShouldThrowDomainException()
    {
        //Arrange
        var client = new ClientDTO("asfdasf", "fasdfa", "13122412", ClientType.NaturalPerson,
            new TypeSpecificPropertiesDTO(null, null, null, null, null));

        //Act and Assert
        await Should.ThrowAsync<DomainException>(_service.UpdateClientAsync(3, client, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateClientAsync_DataForCompanyPropertiesNotGiven_ShouldThrowDomainException()
    {
        //Arrange
        var client = new ClientDTO("asfdasf", "fasdfa", "13122412", ClientType.Company,
            new TypeSpecificPropertiesDTO(null, null, null, null, null));

        //Act and Assert
        await Should.ThrowAsync<DomainException>(_service.UpdateClientAsync(1, client, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateClientAsync_TryingToUpdateNaturalPersonAsCompany_ShouldThrowDomainException()
    {
        //Arrange
        var client = new ClientDTO("asfdasf", "fasdfa", "13122412", ClientType.NaturalPerson,
            new TypeSpecificPropertiesDTO("adfas", "asdf", "125125", null, null));

        //Act and Assert
        await Should.ThrowAsync<DomainException>(_service.UpdateClientAsync(1, client, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateClientAsync_TryingToUpdateCompanyAsNaturalPerson_ShouldThrowDomainException()
    {
        //Arrange
        var client = new ClientDTO("asfdasf", "fasdfa", "13122412", ClientType.Company,
            new TypeSpecificPropertiesDTO(null, null, null, "Company One", "1231412"));

        //Act and Assert
        await Should.ThrowAsync<DomainException>(_service.UpdateClientAsync(3, client, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateClientAsync_NaturalPersonWithIdClientThatDoesntExist_ShouldThrowDomainException()
    {
        //Arrange
        var client = new ClientDTO("asfdasf", "fasdfa", "13122412", ClientType.NaturalPerson,
            new TypeSpecificPropertiesDTO("adfas", "asdf", "12345678901", null, null));

        //Act and Assert
        await Should.ThrowAsync<DomainException>( _service.UpdateClientAsync(31231, client, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateClientAsync_CompanyWithIdNotInDatabase_ShouldThrowDomainException()
    {
        //Arrange
        var client = new ClientDTO("asfdasf", "fasdfa", "13122412", ClientType.Company,
            new TypeSpecificPropertiesDTO(null, null, null, "Company One", "1234567890"));

        //Act and Assert
        await Should.ThrowAsync<DomainException>(_service.UpdateClientAsync(123123,client, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateClientAsync_NaturalPersonInDatabase_ShouldReturnClientViewDTO()
    {
        //Arrange
        var client = new ClientDTO("asfdasf", "fasdfa", "13122412", ClientType.NaturalPerson,
            new TypeSpecificPropertiesDTO("adfas", "asdf", "4412414124", null, null));

        //Act and Assert
        await Should.NotThrowAsync(_service.UpdateClientAsync(3,client, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateClientAsync_CompanyInDatabase_ShouldReturnClientViewDTO()
    {
        //Arrange
        var client = new ClientDTO("asfdasf", "fasdfa", "13122412", ClientType.Company,
            new TypeSpecificPropertiesDTO(null, null, null, "Company One", "1231244423"));

        //Act and Assert
        await Should.NotThrowAsync(_service.UpdateClientAsync(1,client, CancellationToken.None));
    }

    [Fact]
    public async Task GetClientAsync_ClientWithIdNotInDatabase_ShouldThrowDomainException()
    {
        //Arrange
        var IdClient = 123123;
        //Act and Assert
        var result =  _service.GetClientAsync(IdClient, CancellationToken.None);
        await Should.ThrowAsync<DomainException>(result);
    }



    [Fact]
    public async Task GetClientAsync_NaturalPersonInDatabase_ShouldReturnClientViewDTO()
    {
        //Arrange
        var IdClient = 3;

        //Act and Assert
        await Should.NotThrowAsync(_service.GetClientAsync(IdClient, CancellationToken.None));
    }

    [Fact]
    public async Task GetClientAsync_CompanyInDatabase_ShouldReturnClientViewDTO()
    {
        //Arrange
        var IdClient = 1;
        //Act and Assert
        await Should.NotThrowAsync(_service.GetClientAsync(1, CancellationToken.None));
    }

}