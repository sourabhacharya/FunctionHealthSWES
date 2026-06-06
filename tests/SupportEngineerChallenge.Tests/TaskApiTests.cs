using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
/* 
Added 5 missing "using" directives at the top of the test file. 
Adding them fixes the compile errors and lets the test code reference Fact, Task, List<T>, DateTime, and HttpClient/extension methods correctly.
 */
using Xunit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SupportEngineerChallenge.Tests;

public class TaskApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public TaskApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CreateTask_ShouldReturn201_WhenValid()
    {
        var client = _factory.CreateClient();

        var req = new { userId = "user-001", title = "Test task" };

        client.DefaultRequestHeaders.Add("X-Client-Timestamp", DateTime.UtcNow.ToString("O"));

        var res = await client.PostAsJsonAsync("/api/tasks", req);

        res.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task ListTasks_ShouldReturnOnlyRequestedUser()
    {
        var client = _factory.CreateClient();

        var user1 = await client.GetFromJsonAsync<List<TaskDto>>("/api/tasks?userId=user-001&limit=50");
        user1.Should().NotBeNull();
        user1!.Should().OnlyContain(t => t.UserId == "user-001");
    }

    public record TaskDto(int Id, string UserId, string Title, string Status, DateTime CreatedAt, DateTime UpdatedAt);
}
