using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Infrastructure.Data.Models;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    public class CustomerRepository
    {
        public AdventureworksContext UnitOfWork { get; set; }

        public CustomerRepository(AdventureworksContext dbContext)
        {
            UnitOfWork = dbContext;
        }

        public object[] Get()
        {
            return UnitOfWork.Customer
                  .Select(p => new
                  {
                      p.CustomerId,
                      FirstName = p.FirstName,
                      p.MiddleName,
                      p.LastName,
                      Title = p.Title,
                      NameStyle = p.NameStyle
                  })
                          .ToArray();
        }

        public object Get(int id)
        {

            var vquery = UnitOfWork.Customer
                .Where(p => p.CustomerId == id)
                .Select(p => new
                {
                    p.CustomerId,
                    FirstName = p.FirstName,
                    p.MiddleName,
                    p.LastName,
                    Title = p.Title,
                    NameStyle = p.NameStyle
                })
                .FirstOrDefault();
            return vquery;
        }

        public int Save(CustomerViewModel data)
        {
            // Store in DB
            //Products.Add(name);

            /*var query = UnitOfWork.Set<Customer>().AsQueryable();

            var next = query.Max(p => p.ProductNumber) + 1;*/

            var model = new Customer
            {
                    CustomerId = data.CustomerId,
                    FirstName = data.FirstName,
                    MiddleName = data.MiddleName,
                    LastName= data.LastName,
                    Title = data.Title,
                    NameStyle = data.NameStyle
            };

            UnitOfWork.Set<Customer>().Add(model);

            UnitOfWork.SaveChanges();

            return model.CustomerId;

        }

        public bool Update(int id, CustomerViewModel request)
        {
            Customer model = UnitOfWork.Customer.Find(id);

            if (model == null)
            {
                return false;
            }

            model.CustomerId = request.CustomerId;
            model.FirstName = request.FirstName;
            model.MiddleName = request.MiddleName;
            model.LastName = request.LastName;
            model.Title = request.Title;
            model.NameStyle = request.NameStyle;

            UnitOfWork.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            UnitOfWork.SaveChanges();

            return true;
        }

        public bool Delete(int id, CustomerViewModel request)
        {
            var model = UnitOfWork.Set<Customer>().Find(id);
            if (model == null)
            {
                return false;
            }

            UnitOfWork.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            UnitOfWork.SaveChanges();

            return true;
        }
    }
}