using System;

namespace TurboShop.models
{
    public class Post
    {
        public int Id { get; set; }
        public int GeneralType { get; set; }
        public int Brand { get; set; }
        public int Model { get; set; }
        public int BanType { get; set; }
        public string City { get; set; }
        public string Condition { get; set; }
        public string Year { get; set; }
        public string Kilometraj { get; set; }
        public string Info { get; set; }
        public string SharingTime { get; set; }
        public int Order { get; set; }
        public int UserID { get; set; }
        public decimal Price { get; set; }  

        public Post(int id, int generalType, int brand, int model, int banType, string city, string condition, string year, string kilometraj, string info, string sharingTime, int order, int userId, decimal price)
        {
            Id = id;
            GeneralType = generalType;
            Brand = brand;
            Model = model;
            BanType = banType;
            City = city;
            Condition = condition;
            Year = year;
            Kilometraj = kilometraj;
            Info = info;
            SharingTime = sharingTime;
            Order = order;
            UserID = userId;
            Price = price;
        }
    }
}
