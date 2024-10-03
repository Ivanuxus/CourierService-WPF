using CourierService.Models.Entities;
using System.Collections.Generic;

namespace CourierService.Services
{
    public class UserService
    {
        private List<User> _users = new List<User>();

        public void Register(string username, string password)
        {
            // Проверка на существование пользователя
            if (_users.Exists(u => u.Username == username))
            {
                throw new System.Exception("Пользователь с таким именем уже существует.");
            }

            _users.Add(new User { Username = username, Password = password });
        }

        public bool Authenticate(string username, string password)
        {
            var user = _users.Find(u => u.Username == username && u.Password == password);
            return user != null;
        }
    }
}
