using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

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
using WebApplicationAR.Models;
using System.Web.Http.Controllers;

namespace WebApplicationAR.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }

    public class ProductRepository : IDisposable
    {
        private Model1 db = new Model1();
        ApiController ap;
        IHttpController h;

        public IQueryable<Product> Get()
        {
            return db.Product;
        }

        public Product GetProduct(int id)
        {
            return db.Product.FirstOrDefault(i => i.ProId == id);                     
        }

        public List<Product> GetProductByCategory(int category,int type)
        {
            if(type == 0)
            {
                return db.Product.Where(i => i.Category == category).OrderBy(i => i.Name).ToList();
            }           
            return db.Product.Where(i => i.Category == category).OrderByDescending(i => i.Name).ToList();                                    
        }


        public List<Product> GetProductByName(string name)
        {
            return db.Product.Where(i => i.Name == name).OrderByDescending(d => d.Name).ToList();
        }

        public List<Product> GetProductByDescripcion(string desc)
        {            
            return db.Product.Where(i => i.Description == desc).OrderByDescending(d => d.Name).ToList();
        }

        public Product PostProduct(Product product)
        {
            db.Product.Add(product);
            try
            {
                db.SaveChanges();                
            }
            catch(DbUpdateException e)
            {
                throw;
            }
            return product;
        }

        public Product DeleteProduct(int id)
        {
            Product product = db.Product.FirstOrDefault(i => i.ProId == id);
            try
            {
                db.Product.Remove(product);
                db.SaveChanges();
            }
            catch(DbUpdateException e)
            {
                throw;
            }            
            return product;
        }

        public Product PutProduct(int id, Product product)
        {

            if (id != product.ProId)
            {
                return null;
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
               throw;              
            }
            return product;
        }

        private bool ProductExists(int id)
        {
            return db.Product.Count(e => e.ProId == id) > 0;
        }


        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
