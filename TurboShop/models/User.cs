using System;
using System.Collections.Generic;

namespace TurboShop.models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public User(int id, string name, string surname, string phone, string email, string password)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Phone = phone;
            Email = email;
            Password = password;
        }
    }
}
