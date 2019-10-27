using DealDouble.Data;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDoubleServices
{
    public class SharedServices
    {
        public int SavePicture(Picture picture)
        {
            DealDoubleContext context = new DealDoubleContext();
            context.Pictures.Add(picture);
            context.SaveChanges();

            return (picture.ID);
        }
    }
}
