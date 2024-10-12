using System;

namespace TurboShop.models
{
    public class GeneralType
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public GeneralType(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
