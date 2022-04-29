using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using VendingMachineWeb.Entities;
using VendingMachineWeb.Models;

namespace VendingMachineWeb.Controllers
{
    public class VendingMachineCoinsController : ApiController
    {
        private VendingMachines_v3Entities db = new VendingMachines_v3Entities();

        // GET: api/VendingMachineCoins
        public IHttpActionResult GetVendingMachineCoins()
        {
            // return db.VendingMachineCoins;
            List<VendingMachineCoins> VMcoins = db.VendingMachineCoins.ToList();
            List<VendingMachineCoin> list = new List<VendingMachineCoin>();

            foreach (VendingMachineCoins CoinsInMachine in VMcoins)
            {
                string isActive = "";
                Coins coin = db.Coins.SingleOrDefault(x => x.Id == CoinsInMachine.CoinsId);

                if (CoinsInMachine.IsActive == 1)
                    isActive = "Да";
                else isActive = "Нет";

                VendingMachineCoin vendingMachineCoin = new VendingMachineCoin()
                {
                    Id = CoinsInMachine.CoinsId,
                    Denomination = coin.Denomination.ToString(),
                    Count = CoinsInMachine.Count,
                    IsActive = isActive
                };
                list.Add(vendingMachineCoin);
            }
            return Ok(list);
        }

        // GET: api/VendingMachineCoins/5
        [ResponseType(typeof(VendingMachineCoins))]
        public IHttpActionResult GetVendingMachineCoins(int id)
        {
            VendingMachineCoins vendingMachineCoins = db.VendingMachineCoins.Find(id);
            if (vendingMachineCoins == null)
            {
                return NotFound();
            }
            string isActive = "";
            var VMcoins = db.VendingMachineCoins.SingleOrDefault(x => x.Id == id);
            var coin = db.Coins.SingleOrDefault(x => x.Id == VMcoins.CoinsId);
            var VMCoins = db.VendingMachineCoins.SingleOrDefault(x => x.Id == id);
            if (VMCoins.IsActive == 1)
                isActive = "Да";
            else isActive = "Нет";

            VendingMachineCoin vendingMachineCoin = new VendingMachineCoin()
            {
                Id = vendingMachineCoins.Id,
                Count = vendingMachineCoins.Count,
                Denomination = coin.Denomination.ToString(),
                IsActive = isActive
            };

            return Ok(vendingMachineCoin);
        }

        // PUT: api/VendingMachineCoins/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendingMachineCoins(int id, VendingMachineCoins vendingMachineCoins)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendingMachineCoins.Id)
            {
                return BadRequest();
            }

            db.Entry(vendingMachineCoins).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendingMachineCoinsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/VendingMachineCoins
        [ResponseType(typeof(VendingMachineCoins))]
        public IHttpActionResult PostVendingMachineCoins(VendingMachineCoins vendingMachineCoins)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(vendingMachineCoins).State = EntityState.Modified;
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = vendingMachineCoins.Id }, vendingMachineCoins);
        }

        // DELETE: api/VendingMachineCoins/5
        [ResponseType(typeof(VendingMachineCoins))]
        public IHttpActionResult DeleteVendingMachineCoins(int id)
        {
            VendingMachineCoins vendingMachineCoins = db.VendingMachineCoins.Find(id);
            if (vendingMachineCoins == null)
            {
                return NotFound();
            }

            db.VendingMachineCoins.Remove(vendingMachineCoins);
            db.SaveChanges();

            return Ok(vendingMachineCoins);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendingMachineCoinsExists(int id)
        {
            return db.VendingMachineCoins.Count(e => e.Id == id) > 0;
        }
    }
}