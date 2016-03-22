using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyMusic.Models;

namespace MyMusic.ViewModels
{

    //  holds the contents of the user’s shopping cart, 
    public class MusicCartViewModel
    {
        public List<Cart> CartAlbums { get; set; }
        public decimal CartTotal { get; set; }




    }
}