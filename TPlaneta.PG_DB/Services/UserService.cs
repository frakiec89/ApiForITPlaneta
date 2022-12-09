using ForITPlaneta.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPlaneta.PG_DB.Services
{
    public class UserService : IUserService
    {
        public bool AddUser(ForITPlaneta.Core.Model.User user)
        {
			try
            {
                if (IsValidEmail(user.Email) == false)
                    throw new ArgumentException("не корректный email");

                if (CheсkEmailUnique(user.Email) )
                    throw new ArgumentException("Такой email уже существует в базе данных");

                if (CheсkLoginUnique(user.Login) )
                    throw new ArgumentException("Такой логин уже существует в базе данных");

                using PGContext pGContext = new PGContext();
                pGContext.Users.Add(GetNewUSerForDB(user));
                pGContext.SaveChanges();
                return true;    
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
			{
                throw new Exception("Ошибка при  получении  данных из БД\n" + ex.Message, ex);
            }
        }

        private User GetNewUSerForDB(ForITPlaneta.Core.Model.User user )
        {
            var us = new User();
            us.UserName = user.UserName;
            us.Password = user.Password;
            us.Email = user.Email;
            us.Login = user.Login;
            return us;
        }

        public  bool CheсkEmailUnique( string email)
        {
            try
            {
                using PGContext pGContext = new PGContext();
                var r =  pGContext.Users.Any(x => x.Email == email);
                return r;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при  получении  данных из БД\n" + ex.Message, ex);
            }
        }

        public bool CheсkLoginUnique(string login)
        {
            try
            {
                using PGContext pGContext = new PGContext();
                var r =  pGContext.Users.Any(x => x.Login == login);
                return r;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при  получении  данных из БД\n" + ex.Message, ex);
            }
        }

        public bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
