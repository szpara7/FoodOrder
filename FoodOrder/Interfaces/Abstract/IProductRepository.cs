﻿using FoodOrder.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrder.Interfaces.Abstract
{
    interface IProductRepository : IRepository<Product>
    {
    }
}
