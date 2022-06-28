namespace Basket.BusinessLayer
{
    public interface ISessionService
    {
        string CreateNewSession();

        bool CheckExpirationDate(string sessionId);
    }
}
