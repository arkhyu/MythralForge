using MythralForge.Integration.Tests.Fixtures;
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
}