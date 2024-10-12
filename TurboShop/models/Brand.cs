using System;

namespace TurboShop.models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GeneralTypeId { get; set; }

        public Brand(int id, string title, int generalTypeId)
        {
            Id = id;
            Title = title;
            GeneralTypeId = generalTypeId;
        }
    }
}
