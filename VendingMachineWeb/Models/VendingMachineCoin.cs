using VendingMachineWeb.Entities;

namespace VendingMachineWeb.Models
{
    public class VendingMachineCoin
    {
        private string _denomination;
        public int Id { get; set; }
       // public int VendingMachineId { get; set; }
       // public int CoinsId { get; set; }
        public string Denomination
        {
            get { return _denomination; }
            set { _denomination = value; }
        }
        public int Count { get; set; }
        public string IsActive { get; set; }

        //public VendingMachineCoin(VendingMachineCoins CoinsInMachine)
        //{
        //    Id = CoinsInMachine.Id;
        //    VendingMachineId = CoinsInMachine.VendingMachineId;
        //    CoinsId = CoinsInMachine.CoinsId;
        //    Count = CoinsInMachine.Count;
        //    IsActive = CoinsInMachine.IsActive;
        //}
    }
}