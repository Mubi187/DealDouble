using DealDouble.Data;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDoubleServices
{
   public class CategoryService
    {
        public List<Category> GetAllCategories()
        {
            DealDoubleContext context = new DealDoubleContext();
            return context.Categories.ToList();
        }

    }
}
