using VendingMachineWeb.Entities;

namespace VendingMachineWeb.Models
{
    public class Coin
    {
        public int Id { get; set; }
        public int Denomination { get; set; }

        public Coin(Coins Coins)
        {
            Id = Coins.Id;
            Denomination = Coins.Denomination;
        }
    }
}