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
        public static List<tblelement> GetElementByType(string type)
        {
            try
            {
                using (var context = new CoreDbContext())
                {
                    
                   // var elements = context.tblelement.Join(context.tblfav,x=>x.mguid,y=>y.mguid,(x,y)=>new )
                    var elements = context.tblelement.Where(x => x.mtype == type);
                    //var elements = context.tblelement.Select<tblelement>(x => x.mtype == type);
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
        public static void AddElement(tblelement el)
        {
            try
            {
                using (var context = new CoreDbContext())
                {
                   // context.Add<tblelement>(el);
                    context.tblelement.Add(el);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //
            }
        }
    }
}
