namespace CampusManagement.Business.Security
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(Domain.Entities.Person person);
        RefreshToken TakeRefreshToken(string token);
        void RevokeRefreshToken(string token);
    }
}
