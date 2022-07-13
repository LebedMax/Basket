namespace Basket.BusinessLayer.Interface
{
    public interface ISessionService
    {
        string CreateNewSession();

        bool CheckExpirationDate(string sessionId);
    }
}
