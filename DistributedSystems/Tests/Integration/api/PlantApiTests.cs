using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Public.DTO.v1;
using Public.DTO.v1.Identity;
using Xunit.Abstractions;

namespace Tests.Integration.api;

public class PlantApiTests : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;
    
    private readonly JsonSerializerOptions camelCaseJsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };


    public PlantApiTests(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    private async Task<string> RegisterNewUser(string email, string password, string firstname, string lastname)
    {
        var URL = $"/api/v1/identity/account/register";

        var registerData = new
        {
            Email = email,
            Password = password,
            Firstname = firstname,
            Lastname = lastname,
        };

        var data = JsonContent.Create(registerData);
        // Act
        var response = await _client.PostAsync(URL, data);

        var responseContent = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.Equal(true, response.IsSuccessStatusCode);

        return responseContent;
    }

    public async Task<JWTResponse> GetJwt()
    {
        // Arrange
        const string email = "user@test.ee";
        const string firstname = "TestFirst";
        const string lastname = "TestLast";
        const string password = "Foo.bar1";

        var jwt=  await RegisterNewUser(email, password, firstname, lastname);
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, camelCaseJsonSerializerOptions);

        return jwtResponse!;
        
    }

    [Fact(DisplayName = "GET - plant")]
    public async Task ApiPlantTestGet()
    {
        var jwtResponse = await GetJwt();

        // Act
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Plant");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);
        var response = await _client.SendAsync(request);

        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());

        // Assert
        response.EnsureSuccessStatusCode();
        
        
    }
    
    [Fact(DisplayName = "POST - plant")]
    public async Task ApiPlantTestPost()
    {
        var jwtResponse = await GetJwt();

        // Act
        var size = new SizeCategory()
        {
            SizeName = "Large"
        };
        var sizeCatData = JsonContent.Create(size);

        var sizeCategoryRequest = new HttpRequestMessage(HttpMethod.Post, "/api/v1/SizeCategory");
        sizeCategoryRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);
        sizeCategoryRequest.Content = sizeCatData;

        var sizeResponse = await _client.SendAsync(sizeCategoryRequest);
        sizeResponse.EnsureSuccessStatusCode();
        
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/Plant");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);
        request.Content = JsonContent.Create(new Plant()
        {
            PlantName = "Test",
            SizeCategory = size
        });
        var response = await _client.SendAsync(request);
        
        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
        
        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    // [Fact(DisplayName = "DELETE - plant")]
    // public async Task ApiPlantTestDelete()
    // {
    //     var jwtResponse = await GetJwt();
    //     
    //     // Act
    //     var size = new SizeCategory()
    //     {
    //         SizeName = "Large"
    //     };
    //     var sizeCatData = JsonContent.Create(size);
    //     
    //     var sizeCategoryRequest = new HttpRequestMessage(HttpMethod.Post, "/api/v1/SizeCategory");
    //     sizeCategoryRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);
    //     sizeCategoryRequest.Content = sizeCatData;
    //     
    //     var sizeResponse = await _client.SendAsync(sizeCategoryRequest);
    //     sizeResponse.EnsureSuccessStatusCode();
    //     
    //     var plant = new Plant()
    //     {
    //         Id = Guid.NewGuid(),
    //         PlantName = "Test",
    //         SizeCategory = size
    //     };
    //     var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/Plant");
    //     request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);
    //     request.Content = JsonContent.Create(plant);
    //     var response = await _client.SendAsync(request);
    //     
    //     _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    //     
    //     // Assert
    //     response.EnsureSuccessStatusCode();
    //     
    //     _testOutputHelper.WriteLine(plant.Id.ToString());
    //
    //     _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);
    //     var deleteRequestResponse = await _client.DeleteAsync($"/api/v1/Plant/{plant.Id}");
    //     
    //
    //     deleteRequestResponse.EnsureSuccessStatusCode();
    // }
}