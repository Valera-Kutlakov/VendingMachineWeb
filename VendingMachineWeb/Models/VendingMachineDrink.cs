using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendingMachineWeb.Entities;

namespace VendingMachineWeb.Models
{
    public class VendingMachineDrink
    {
        private string _nameDrink;
        public int Id { get; set; }
        public string NameDrink
        {
            get { return _nameDrink; }
            set { _nameDrink = value; }
        }
        public int Count { get; set; }

        //public VendingMachineDrink(VendingMachineDrinks DrinkInMachine)
        //{
        //    Id = DrinkInMachine.Id;
        //    DrinksId = DrinkInMachine.DrinksId;
        //    VendingMachineId = DrinkInMachine.VendingMachineId;
        //    Count = DrinkInMachine.Count;
        //}
    }
}