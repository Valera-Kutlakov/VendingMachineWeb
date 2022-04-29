using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using VendingMachineWeb.Entities;
using VendingMachineWeb.Models;

namespace VendingMachineWeb.Controllers
{
    public class VendingMachineDrinksController : ApiController
    {
        private VendingMachines_v3Entities db = new VendingMachines_v3Entities();

        // GET: api/VendingMachineDrinks
        public IHttpActionResult GetVendingMachineDrinks()
        {
            //return db.VendingMachineDrinks;
            List<VendingMachineDrinks> drinkInMachine = db.VendingMachineDrinks.ToList();
            List<VendingMachineDrink> list = new List<VendingMachineDrink>();

            foreach(VendingMachineDrinks drinkMachine in drinkInMachine)
            {
                Drinks drink = db.Drinks.SingleOrDefault(x => x.Id == drinkMachine.DrinksId);
                VendingMachineDrink vendingMachineDrink = new VendingMachineDrink()
                {
                  Id = drinkMachine.Id,
                  NameDrink = drink.Name,
                  Count = drinkInMachine.Count
                };
                list.Add(vendingMachineDrink);
            }
            return Ok(list);
        }

        // GET: api/VendingMachineDrinks/5
        [ResponseType(typeof(VendingMachineDrinks))]
        public IHttpActionResult GetVendingMachineDrinks(int id)
        {
            VendingMachineDrinks vendingMachineDrinks = db.VendingMachineDrinks.Find(id);
            if (vendingMachineDrinks == null)
            {
                return NotFound();
            }
            var VMdrink = db.VendingMachineDrinks.SingleOrDefault(x => x.Id == id);
            var drink = db.Drinks.SingleOrDefault(x => x.Id == VMdrink.DrinksId);
            VendingMachineDrink vendingMachineDrink = new VendingMachineDrink()
            {
                Id = vendingMachineDrinks.Id,
                NameDrink = drink.Name,
                Count = vendingMachineDrinks.Count
            };
            return Ok(vendingMachineDrink);
        }

        // PUT: api/VendingMachineDrinks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendingMachineDrinks(int id, VendingMachineDrinks vendingMachineDrinks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendingMachineDrinks.Id)
            {
                return BadRequest();
            }

            db.Entry(vendingMachineDrinks).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendingMachineDrinksExists(id))
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

        // POST: api/VendingMachineDrinks
        [ResponseType(typeof(VendingMachineDrinks))]
        public IHttpActionResult PostVendingMachineDrinks(VendingMachineDrinks vendingMachineDrinks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VendingMachineDrinks.Add(vendingMachineDrinks);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendingMachineDrinks.Id }, vendingMachineDrinks);
        }

        // DELETE: api/VendingMachineDrinks/5
        [ResponseType(typeof(VendingMachineDrinks))]
        public IHttpActionResult DeleteVendingMachineDrinks(int id)
        {
            VendingMachineDrinks vendingMachineDrinks = db.VendingMachineDrinks.Find(id);
            if (vendingMachineDrinks == null)
            {
                return NotFound();
            }

            db.VendingMachineDrinks.Remove(vendingMachineDrinks);
            db.SaveChanges();

            return Ok(vendingMachineDrinks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendingMachineDrinksExists(int id)
        {
            return db.VendingMachineDrinks.Count(e => e.Id == id) > 0;
        }
    }
}