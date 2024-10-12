using System;

namespace TurboShop.models
{
    public class BanType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GeneralTypeId { get; set; }

        public BanType(int id, string title, int generalTypeId)
        {
            Id = id;
            Title = title;
            GeneralTypeId = generalTypeId;
        }
    }
}
