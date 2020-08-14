﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    public class ProductRepository
    {
        private readonly List<string> Products = new List<string>
        {
            "Jeans", "T-shirt", "Pants"
        };

        public object[] Get() 
        {
            var i = 0;

            return Products.Select(model => new {
                Name = model,
                Id = i++
            }).ToArray();
        }

        public object Get(int id)
        {
            var i = 0;

            var result = Products.Select(model => new {
                Name = model,
                Id = i++
            }).ToList();

            return result.ElementAtOrDefault(id);
        }

        public void Save(string name)
        {
            // Store in DB
            Products.Add(name);
        }
    }
}