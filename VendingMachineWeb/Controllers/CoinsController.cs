using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using VendingMachineWeb.Entities;
using VendingMachineWeb.Models;

namespace VendingMachineWeb.Controllers
{
    public class CoinsController : ApiController
    {
        private VendingMachines_v3Entities db = new VendingMachines_v3Entities();

        // GET: api/Coins
        public IHttpActionResult GetCoins()
        {
            // return db.Coins;
            List<Coins> coins = db.Coins.ToList();
            List<Coin> list = new List<Coin>();

            foreach (Coins coin in coins)
            {
                list.Add(new Coin(coin));
            }
            return Ok(list);
        }

        // GET: api/Coins/5
        [ResponseType(typeof(Coins))]
        public IHttpActionResult GetCoins(int id)
        {
            Coins coins = db.Coins.Find(id);
            if (coins == null)
            {
                return NotFound();
            }

            return Ok(new Coin(coins));
        }

        // PUT: api/Coins/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoins(int id, Coins coins)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coins.Id)
            {
                return BadRequest();
            }

            db.Entry(coins).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoinsExists(id))
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

        // POST: api/Coins
        [ResponseType(typeof(Coins))]
        public IHttpActionResult PostCoins(Coins coins)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Coins.Add(coins);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = coins.Id }, coins);
        }

        // DELETE: api/Coins/5
        [ResponseType(typeof(Coins))]
        public IHttpActionResult DeleteCoins(int id)
        {
            Coins coins = db.Coins.Find(id);
            if (coins == null)
            {
                return NotFound();
            }

            db.Coins.Remove(coins);
            db.SaveChanges();

            return Ok(coins);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoinsExists(int id)
        {
            return db.Coins.Count(e => e.Id == id) > 0;
        }
    }
}