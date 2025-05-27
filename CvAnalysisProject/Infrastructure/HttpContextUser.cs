using CvAnalysisSystem.Application.Services.Abstract;
using System.Security.Claims;

namespace CvAnalysisSystemProject.Infrastructure;

public class HttpContextUser : IUserContext
{
    private readonly int _id;

    public HttpContextUser(IHttpContextAccessor httpContextAccessor)
    {
        var id = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

        _id = Int32.Parse(id);
    }

    //public HttpContextUser(IHttpContextAccessor httpContextAccessor)
    //{
    //    var httpContext = httpContextAccessor.HttpContext;
    //    if (httpContext == null)
    //        throw new InvalidOperationException("HttpContext is not available.");

    //    var user = httpContext.User;
    //    if (user == null || !user.Identity.IsAuthenticated)
    //        throw new InvalidOperationException("User is not authenticated.");

    //    var claim = user.FindFirst(ClaimTypes.NameIdentifier);
    //    if (claim == null)
    //        throw new InvalidOperationException("NameIdentifier claim not found.");

    //    if (!int.TryParse(claim.Value, out _id))
    //        throw new InvalidOperationException("Invalid user ID claim value.");
    //}

    int? IUserContext.UserId => _id;

    public int MustGetUserId()
    {
        if (_id <= 0)
            throw new InvalidOperationException("User has to login");

        return _id;
    }
}
