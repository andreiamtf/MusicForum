using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMusic.Models
{
    [MetadataType(typeof(CartMetaData))]
    public partial class Cart
    {
        public class CartMetaData
        
       
        {
            [Key]
            public int RecordId { get; set; }
        }
    }
}