using MythralForge.Integration.Tests.Fixtures;
using System.Net;
using System.Net.Http.Json;

namespace MythralForge.Integration.Tests.Auth;

public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_ReturnsOk_WhenUserIsValid()
    {
        var request = new
        {
            Email = "testuser@example.com",
            Password = "P@ssw0rd123!",
            ConfirmPassword = "P@ssw0rd123!"
        };

        var response = await _client.PostAsJsonAsync("/api/auth/register", request);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("User registered successfully", content);
    }

    [Fact]
    public async Task Register_ReturnsError_WhenPasswordDoesNotMatch()
    {
        var request = new
        {
            Email = "baduser@example.com",
            Password = "P@ssw0rd12!",
            ConfirmPassword = "P@ssw0rd123!"
        };

        var response = await _client.PostAsJsonAsync("/api/auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Passwords do not match.", content);
    }

    [Theory]
    [InlineData("short")]                   // Too short, no digit, no uppercase
    [InlineData("alllowercase1")]          // No uppercase
    [InlineData("ALLUPPERCASE1")]          // No lowercase
    [InlineData("NoDigitPassword")]        // No digit
    [InlineData("123456")]                 // No letters
    public async Task Register_ReturnsBadRequest_WhenPasswordPolicyIsViolated(string badPassword)
    {
        // Arrange
        var request = new
        {
            Email = $"user_{Guid.NewGuid()}@example.com",
            Password = badPassword,
            ConfirmPassword = badPassword
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("password", content, StringComparison.OrdinalIgnoreCase);
    }
}