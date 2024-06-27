using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Revenue_Recognition_System_Test.TestObjects;
using Revenue_Recognition_System.DTO;
using Revenue_Recognition_System.Exceptions;
using Revenue_Recognition_System.Repositories;
using Revenue_Recognition_System.Services;
using Shouldly;

namespace Revenue_Recognition_System_Test;

public class SecurityServiceTest
{
    private ISecurityService _service;
    private IUserRepository _userRepository;

    public SecurityServiceTest()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _service = new SecurityService(new FakeUnitOfWork(), config, new FakeUserRepository());
        _userRepository = new FakeUserRepository();
    }

    [Fact]
    public async Task RegisterUserAsync_UserInDataBase_ShouldThrowDomainException()
    {
        //Arrange
        var user = new UserDto("Login 1", "pass123");

        //Assure and act
        await Should.ThrowAsync<DomainException>(_service.RegisterUserAsync(user, CancellationToken.None));
    }

    [Fact]
    public async Task RegisterUserAsync_UserNotInDataBase_ShouldNotThrowExceptions()
    {
        //Arrange
        var user = new UserDto("asdfasd1", "pass123");

        //Assure and act
        await Should.NotThrowAsync(_service.RegisterUserAsync(user, CancellationToken.None));
    }
    
    [Fact]
    public async Task LogInAsync_UserNotInDataBase_ShouldThrowDomainException()
    {
        //Arrange
        var user = new UserDto("Logasd", "pass123");

        //Assure and act
        await Should.ThrowAsync<DomainException>(_service.LogInAsync(user, CancellationToken.None));
    }

    [Fact]
    public async Task LogInAsync_WrongPassword_ShouldUnauthorizedAccessException()
    {
        //Arrange
        var user = new UserDto("Login 1", "fasdfas");

        //Assure and act
        await Should.ThrowAsync<UnauthorizedAccessException>(_service.LogInAsync(user, CancellationToken.None));
    }

    [Fact]
    public async Task LogInAsync_GoodPassword_ShouldReturnTokenDTO()
    {
        //Arrange
        var user = new UserDto("Login 1", "Password");

        //Assure and act
        await Should.NotThrowAsync(_service.LogInAsync(user, CancellationToken.None));
    }

    [Fact]
    public async Task RefreshTokenAsync_GoodRefreshToken_ShouldReturnTokenDTO()
    {
        //Arrange
        var user = new UserDto("Login 1", "Password");
        var token = await _service.LogInAsync(user, CancellationToken.None);
        var refreshToken = new RefreshTokenDTO(token.RefreshToken);

        //Assure and act
        await Should.NotThrowAsync(_service.RefreshTokenAsync(refreshToken, CancellationToken.None));
    }

    [Fact]
    public async Task RefreshTokenAsync_BadRefreshToken_ShouldReturnTokenDTO()
    {
        //Arrange
        var refreshToken = new RefreshTokenDTO("fasdfasdjkfjaslkdjf;aksjdfklajs");

        //Assure and act
        await Should.ThrowAsync<SecurityTokenException>(_service.RefreshTokenAsync(refreshToken, CancellationToken.None));
    }

}