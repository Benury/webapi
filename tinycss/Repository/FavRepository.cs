using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCSS_Webapi.Models;

namespace TinyCSS_Webapi.Repository
{
    public class FavRepository
    {
        public static List<tblfav> Get()
        {
            try
            {
                using (var context = new CoreDbContext())
                {
                    var favs = context.tblfav;
                    List<tblfav> items = new List<tblfav>();
                    foreach (var item in favs)
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
        public static List<tblfav> GetFavByUserid(string userid)
        {
            try
            {
                using (var context = new CoreDbContext())
                {
                    var favs = context.tblfav.Where(x => x.userid == userid);
                    //var elements = context.tblfav.Select<tblfav>(x => x.mtype == type);
                    List<tblfav> items = new List<tblfav>();
                    foreach (var item in favs)
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
        public static List<tblfav> GetFavByUseridAndType(string userid,string type)
        {
            try
            {
                using (var context = new CoreDbContext())
                {
                    var favs = context.tblfav.Where(x => x.userid == userid).Where(x => x.mtype == type);
                    //var elements = context.tblfav.Select<tblfav>(x => x.mtype == type);
                    List<tblfav> items = new List<tblfav>();
                    foreach (var item in favs)
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
        public static tblfav AddFav(tblfav fav)
        {
            try
            {
                using (var context = new CoreDbContext())
                {
                    // context.Add<tblelement>(fav);
                    var favs = context.tblfav.Where(x => x.userid == fav.userid ).Where(x=>x.mguid == fav.mguid);
                    if (favs.Count() > 0)
                    {
                        context.tblfav.Remove(favs.First());
                    }
                    else
                    {
                        context.tblfav.Add(fav);
                    }
                    //foreach (var item in favs)
                    //{
                    //    context.tblfav.Remove(item);
                       
                    //    fav = item;
                    //}
                    
                   
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            
            return fav;
           
        }
        public static void DelFav(int fguid)
        {
            try
            {
                using (var context = new CoreDbContext())
                {
                    // context.Add<tblelement>(fav);
                    var fav = context.tblfav.Where(x => x.fguid == fguid).FirstOrDefault<tblfav>();
                    context.tblfav.Remove(fav);
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
