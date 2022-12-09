namespace ForITPlaneta.Core
{
    public interface IAuthServices
    {
        string GetTokken(string login, string password);
        bool Login(string login, string password);
        bool IsChekedTokken(string tokken); 
    }
}