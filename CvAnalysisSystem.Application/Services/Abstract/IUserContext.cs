namespace CvAnalysisSystem.Application.Services.Abstract;
public interface IUserContext
{
    public int? UserId { get; }

    public int MustGetUserId();
}

