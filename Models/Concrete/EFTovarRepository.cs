using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Models.Repository;

namespace WebSite.Models.Concrete
{
    public class EFTovarRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Tovar> Tovars
        {
            get { return context.Tovars; }
        }
        public void SaveTovar(Tovar tovar)
        {
            if (tovar.Tovar_ID == 0)
                context.Tovars.Add(tovar);
            else
            {
                Tovar dbEntry = context.Tovars.Find(tovar.Tovar_ID);
                if (dbEntry != null)
                {
                    dbEntry.Category = tovar.Category;
                    dbEntry.Volume = tovar.Volume;
                    dbEntry.Price = tovar.Price;
                    dbEntry.HOT = tovar.HOT;
                    dbEntry.Pict = tovar.Pict;
                    dbEntry.Dimension = tovar.Dimension;
                }
            }
            context.SaveChanges();
        }


    }
}