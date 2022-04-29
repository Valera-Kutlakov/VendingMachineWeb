using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using VendingMachineWeb.Entities;
using VendingMachineWeb.Models;
using Newtonsoft.Json;

namespace VendingMachineWeb.Controllers
{
    public class DrinksController : ApiController
    {
        private VendingMachines_v3Entities db = new VendingMachines_v3Entities();

        // GET: api/Drinks
        public IHttpActionResult GetDrinks()
        {
            List<Drinks> drinks = db.Drinks.ToList();
            List<Drink> list = new List<Drink>();

            foreach (Drinks drink in drinks)
            {
                list.Add(new Drink(drink));
            }
           
            return Json(list);

        }

        // GET: api/Drinks/5
        [ResponseType(typeof(Drinks))]
        public IHttpActionResult GetDrinks(int id)
        {
            Drinks drinks = db.Drinks.Find(id);
            if (drinks == null)
            {
                return NotFound();
            }
            return Json(new Drink(drinks));
        }

        // PUT: api/Drinks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDrinks(int id, Drinks drinks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drinks.Id)
            {
                return BadRequest();
            }

            db.Entry(drinks).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinksExists(id))
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

        // POST: api/Drinks
        [ResponseType(typeof(Drinks))]
        public IHttpActionResult PostDrinks(Drinks drinks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Drinks.Add(drinks);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = drinks.Id }, drinks);
        }

        // DELETE: api/Drinks/5
        [ResponseType(typeof(Drinks))]
        public IHttpActionResult DeleteDrinks(int id)
        {
            Drinks drinks = db.Drinks.Find(id);
            if (drinks == null)
            {
                return NotFound();
            }

            db.Drinks.Remove(drinks);
            db.SaveChanges();

            return Ok(drinks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DrinksExists(int id)
        {
            return db.Drinks.Count(e => e.Id == id) > 0;
        }
    }
}