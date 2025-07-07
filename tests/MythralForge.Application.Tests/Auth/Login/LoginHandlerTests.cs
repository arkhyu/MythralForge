using Moq;
public class LoginHandlerTests
{
    private readonly Mock<IAuthenticationService> _authenticationServiceMock;
    private readonly LoginHandler _loginHandler;

    public LoginHandlerTests()
    {
        // Arrange: Mock the IAuthenticationService dependency
        _authenticationServiceMock = new Mock<IAuthenticationService>();
        _loginHandler = new LoginHandler(_authenticationServiceMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnOutcomeResult_WhenCalledWithValidCommand()
    {
        // Arrange
         var email = "test@example.com";
        var password = "password123";
        var command = new LoginCommand(email,password);

        var expectedOutcome = new OutcomeResult(true,new List<string>{"Login successful"});
        _authenticationServiceMock.Setup(service => service.LoginAsync(command.Email, command.Password))
            .ReturnsAsync((expectedOutcome,"mytoken"));

        // Act
        var result = await _loginHandler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Item1.Success);
        Assert.Equal("Login successful", result.Item1.Errors.First());

        _authenticationServiceMock.Verify(service => service.LoginAsync(command.Email, command.Password), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnFailureOutcomeResult_WhenLoginFails()
    {
        // Arrange
         var email = "test@example.com";
        var password = "wrongpassword";
        var command = new LoginCommand(email,password);

        var expectedOutcome = new OutcomeResult(false,new List<string>{"Invalid credentials"});
        _authenticationServiceMock.Setup(service => service.LoginAsync(command.Email, command.Password))
            .ReturnsAsync((expectedOutcome,null));

        // Act
        var result = await _loginHandler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Item1.Success);
        Assert.Equal("Invalid credentials", result.Item1.Errors.First());

        _authenticationServiceMock.Verify(service => service.LoginAsync(command.Email, command.Password), Times.Once);
    }
}