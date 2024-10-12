using System;

namespace TurboShop.models
{
    public class Model
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BrandId { get; set; }

        public Model(int id, string title, int brandId)
        {
            Id = id;
            Title = title;
            BrandId = brandId;
        }
    }
}
