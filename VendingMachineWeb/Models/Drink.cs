using VendingMachineWeb.Entities;

namespace VendingMachineWeb.Models
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public decimal Cost { get; set; }

        public Drink(Drinks Drink)
        {
            Id = Drink.Id;
            Name = Drink.Name;
            Image = Drink.Image;
            Cost = Drink.Cost;
        }
    }
}