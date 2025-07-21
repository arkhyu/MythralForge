using Moq;
using MythralForge.Application.Auth.Login;
using MythralForge.Application.Common;

namespace MythralForge.Application.Tests.Auth.Login;

public class LoginHandlerTests
{
    private readonly Mock<IAuthenticationService> _authenticationServiceMock;
    private readonly LoginHandler _loginHandler;

    public LoginHandlerTests()
    {
        _authenticationServiceMock = new Mock<IAuthenticationService>();
        _loginHandler = new LoginHandler(_authenticationServiceMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnSuccessResponse_WhenCalledWithValidCommand()
    {
        // Arrange
         var email = "test@example.com";
        var password = "password123";
        var command = new LoginCommand(email,password);

        var expectedResponse = Response<string>.Success("mytoken");
        _authenticationServiceMock.Setup(service => service.LoginAsync(command.Email, command.Password))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _loginHandler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal("mytoken", result.Data);

        _authenticationServiceMock.Verify(service => service.LoginAsync(command.Email, command.Password), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnFailureResponse_WhenLoginFails()
    {
        // Arrange
         var email = "test@example.com";
        var password = "wrongpassword";
        var command = new LoginCommand(email,password);

        var expectedResponse = Response<string>.Failure("Invalid credentials");
        _authenticationServiceMock.Setup(service => service.LoginAsync(command.Email, command.Password))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _loginHandler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.Equal("Invalid credentials", result.Errors?.First());

        _authenticationServiceMock.Verify(service => service.LoginAsync(command.Email, command.Password), Times.Once);
    }
}