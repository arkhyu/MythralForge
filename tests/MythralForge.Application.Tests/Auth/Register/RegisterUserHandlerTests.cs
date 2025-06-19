using Moq;

public class RegisterUserHandlerTests
{
    [Fact]
    public async Task HandleAsync_ShouldReturnSuccessResult_WhenUserServiceReturnsSuccess()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var email = "test@example.com";
        var password = "password123";
        var expectedSuccess = true;
        var expectedErrors = new string[0];

        mockUserService
            .Setup(s => s.RegisterUserAsync(email, password))
            .ReturnsAsync((expectedSuccess, expectedErrors));

        var handler = new RegisterUserHandler(mockUserService.Object);
        var command = new RegisterCommand(email,password);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(expectedErrors, result.Errors);
        mockUserService.Verify(s => s.RegisterUserAsync(email, password), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnFailureResult_WhenUserServiceReturnsFailure()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var email = "test@example.com";
        var password = "password123";
        var expectedSuccess = false;
        var expectedErrors = new[] { "Email already exists" };

        mockUserService
            .Setup(s => s.RegisterUserAsync(email, password))
            .ReturnsAsync((expectedSuccess, expectedErrors));

        var handler = new RegisterUserHandler(mockUserService.Object);
        var command = new RegisterCommand(email,password);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(expectedErrors, result.Errors);
        mockUserService.Verify(s => s.RegisterUserAsync(email, password), Times.Once);
    }
}
