using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCSS_Webapi.Models;

namespace TinyCSS_Webapi.Repository
{
    public class MElementRepository
    {
        public static List<tblelement> Get()
        {
            try
            {
                using (var context = new CoreDbContext())
                {
                    var elements = context.tblelement;
                    List<tblelement> items = new List<tblelement>();
                    foreach (var item in elements)
                    {
                        items.Add(item);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
