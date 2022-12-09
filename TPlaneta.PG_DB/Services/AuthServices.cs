using ForITPlaneta.Core;
using System.Text;
using TPlaneta.PG_DB;
using System.Security.Cryptography;

namespace TPlaneta.PG_DB.Services
{
    public class AuthServices : IAuthServices
    {
        private const int timeAyt = 5; 

        public bool Login(string login, string password)
        {
            try
            {
                using PGContext pGContext = new PGContext();
                return pGContext.Users.Any(x => x.Login == login && x.Password == password);
            }
            catch (Exception ex)
            {
               throw new Exception("Ошибка при  получении  данных из БД\n" +  ex.Message, ex);
            }
        }

        public string GetTokken(string login, string password)
        {
            if (Login(login, password) == true)
            {
                try
                {
                    using PGContext pGContext = new PGContext();
                    var user = pGContext.Users.Single(x => x.Login == login && x.Password == password);
                    if (IsChekedTokken(user) == true)
                        return pGContext.Sessions.Where(x => x.UserId == user.UserId 
                        
                        ).ToArray().Last().Tokken;
                    else
                    {
                        string newTokken = GetTokkenCript(login, password);
                        pGContext.Sessions.Add(new Session
                        {
                            UserId = user.UserId,
                            Date = DateTime.Now.ToUniversalTime(),
                            Tokken = newTokken
                        });
                        pGContext.SaveChanges();
                        return newTokken;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при  получении  данных из БД\n" + ex.Message, ex);
                }
            }
            else
            {
                throw new ArgumentException ("неверные  дынные  авторизации\n");
            }
           
        }

        private string GetTokkenCript(string login, string password)
        {
            var   tmpHash = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes
                (login + password + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()
                )); 
            return ByteArrayToString(tmpHash);
        }

        private string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length - 1; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }


        public bool IsChekedTokken (string tokken)
        {
            try
            {
                using PGContext pGContext = new PGContext();
                if (pGContext.Sessions.Any(x => x.Tokken == tokken) == false)
                    return false;

                var data = pGContext.Sessions.Where(x => x.Tokken == tokken).Max(x => x.Date);

                return ChekedData(data);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при  получении  данных из БД\n" + ex.Message, ex);
            }
        }

        private static bool ChekedData(DateTime data)
        {
            var d1 = data.ToUniversalTime();
            var d2 = DateTime.Now.ToUniversalTime().AddMinutes(-timeAyt).ToUniversalTime() ;

            if (d1 >= d2)
                return true;
            else
                return false;
        }

        public bool IsChekedTokken(User user )
        {
            try
            {
                using PGContext pGContext = new PGContext();
                if (pGContext.Sessions.Any(x => x.UserId == user.UserId) == false)
                    return false;
                var data = pGContext.Sessions.Where(x => x.UserId == user.UserId).Max(x => x.Date).ToUniversalTime();
                return ChekedData(data);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при  получении  данных из БД\n" + ex.Message, ex);
            }
        }
    }
}
