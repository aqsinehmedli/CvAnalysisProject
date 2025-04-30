using CvAnalysisSystem.Common.GlobalResponses;
using System.Collections.Concurrent;
using System.Net;
using System.Text.Json;

public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly ConcurrentDictionary<string, List<DateTime>> _requestTimes = new();
    private static readonly TimeSpan _timeWindow = TimeSpan.FromMinutes(1); // 1 dəqiqəlik pəncərə
    private const int _maxRequest = 100; // 1 dəqiqədə maksimum 4 sorğu

    public RateLimitMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Müştəriyi IP ünvanı ilə təyin edirik
        string clientKey = context.Connection.RemoteIpAddress.ToString();

        var currentTime = DateTime.UtcNow;

        // Yalnız 1 dəqiqəlik zaman pəncərəsində olan sorğuları saxlayırıq
        if (!_requestTimes.ContainsKey(clientKey))
        {
            _requestTimes[clientKey] = new List<DateTime>();
        }

        var requestList = _requestTimes[clientKey];

        // İstifadəçinin sorğularını vaxt pəncərəsi içərisində saxlayırıq
        requestList.RemoveAll(time => time < currentTime - _timeWindow);

        // İstifadəçi artıq 4 sorğu atıbsa, error qaytarırıq
        if (requestList.Count >= _maxRequest)
        {
            var message = new List<string>() { "Too many requests. Please wait a bit." };
            await WriteError(context, HttpStatusCode.TooManyRequests, message);
            return;
        }

        // Yeni sorğu vaxtını əlavə edirik
        requestList.Add(currentTime);

        // Sorğu limitini keçmədikdə, növbəti middleware-ə keçirik
        await _next(context);
    }

    public static async Task WriteError(HttpContext context, HttpStatusCode statusCode, List<string> messages)
    {
        // Cavab hələ başlamayıbsa, onu təmizləyirik
        if (!context.Response.HasStarted)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(new Result(messages));
            await context.Response.WriteAsync(json);
        }
    }
}

