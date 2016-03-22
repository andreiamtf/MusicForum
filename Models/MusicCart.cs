using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;                       


namespace MyMusic.Models
{
    public partial class MusicCart
    {

        MvcMusicStoreEntities db = new MvcMusicStoreEntities();

        string MusicCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static MusicCart GetMusicCart(HttpContextBase context)
        {
            // unknown user - globally unique identifier (GUID) using the ASP.NET Session class to store the id.
            var cart = new MusicCart();
            cart.MusicCartId = cart.GetMusicCartId(context);
            return cart;
        }


        // HttpContextBase: to know about the current HTTP request (It contains request( User, Session, Profile, Cache) and response objects))
        public string GetMusicCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempMusicCartId = Guid.NewGuid();

                    // the tempMusicCartId is coming back to the client
                    context.Session[CartSessionKey] = tempMusicCartId.ToString();
                }

            }

            return context.Session[CartSessionKey].ToString();
        }


        public void AddMusicCart(Album album)
        {
            // Returns the only album
            var cartAlbum = db.Carts.SingleOrDefault(a=> a.CartId == MusicCartId && a.AlbumId == album.AlbumId);
            
            if (cartAlbum == null)
            {
                // new cart item if there is no cart item
                cartAlbum = new Cart
                {
                    AlbumId = album.AlbumId,
                    CartId = MusicCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                db.Carts.Add(cartAlbum);
            }
            else
            {
                // item exists in the cart - add one to the quantity
                cartAlbum.Count = cartAlbum.Count + 1;
            }

            db.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartAlbum = db.Carts.Single(cart => cart.CartId == MusicCartId && cart.RecordId == id);

            int itemCount = 0;
            if (cartAlbum != null)
            {
                if (cartAlbum.Count > 1)
                {
                    cartAlbum.Count = cartAlbum.Count-1;
                    itemCount = cartAlbum.Count;
                }
                else
                {
                    db.Carts.Remove(cartAlbum);
                }
                db.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyMusicCart()
        {
            var cartAlbums = db.Carts.Where(cart => cart.CartId == MusicCartId);
            foreach (var cartAlbum in cartAlbums)
            {
                db.Carts.Remove(cartAlbum);
            }
            
            db.SaveChanges();
        }
        public List<Cart> GetCartAlbums()
        {
            return db.Carts.Where(cart => cart.CartId == MusicCartId).ToList();
        }
        public int GetCount()
        {
            
            //get the number of each album and sum 
            int? count = (from cartAlbums in db.Carts 
                          where cartAlbums.CartId == MusicCartId
                          select (int?)cartAlbums.Count).Sum();
            // Return 0 if all entries are null (if count!= 0 return count; else  return count = 0})
            return count ?? 0; 
        }
        public decimal GetCartTotal()
        {
            // album price x count(album) and sum album price total

            decimal? total = (from cartAlbums in db.Carts
                              where cartAlbums.CartId == MusicCartId
                              select (int?)cartAlbums.Count * cartAlbums.Album.Price).Sum();
            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            var cartAlbums = GetCartAlbums();

            // add the order details for each album
            foreach (var item in cartAlbums)
            {
                var orderDetail = new OrderDetail
                {
                    AlbumId = item.AlbumId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };
                // order total of the shopping cart
                orderTotal = orderTotal+(item.Count * item.Album.Price);
                db.OrderDetails.Add(orderDetail);
            }
            // order's total to the orderTotal count
            order.Total = orderTotal;
       
            db.SaveChanges();
     
            EmptyMusicCart();
   
            return order.OrderId;
        }


        // migrate the MusicCart (guid) to be associated with the username
        public void MigrateCart(string userName)
        {
            var MusicCart = db.Carts.Where(c => c.CartId == MusicCartId);
            foreach (Cart item in MusicCart)
            {
                item.CartId = userName;
            }
            db.SaveChanges();
        }

    }

}