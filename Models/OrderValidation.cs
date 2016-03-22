using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMusic.Models
{
    [MetadataType(typeof(OrderMetaData))]

    public partial class Order
    {

        public class OrderMetaData
        {

            [Key]
            public int OrderId { get; set; }

            [ScaffoldColumn(false)]
            public DateTime OrderDate { get; set; }

            [DisplayName("User")]
            public string Username { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            public string FirstName { get; set; }

            [DisplayName("Surname")]
            [Required(ErrorMessage = "Surname is required")]
            public string LastName { get; set; }

            [DisplayName("Address")]
            [Required(ErrorMessage = "Address is required")]
            public string Address { get; set; }

            [Required(ErrorMessage = "Postal Code is required")]
            [DisplayName("Postal Code")]
            [StringLength(10)]
            public string PostalCode { get; set; }

            [Required(ErrorMessage = "City is required")]
            [StringLength(40)]
            public string City { get; set; }

            [Required(ErrorMessage = "Country is required")]
            [StringLength(40)]
            public string Country { get; set; }

            public string State { get; set; }

            [Required(ErrorMessage = "Phone is required")]
            [StringLength(24)]
            public string Phone { get; set; }

            [Required(ErrorMessage = "Email Address is required")]
            [DisplayName("Email Address")]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is is not valid.")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [ScaffoldColumn(false)]
            public decimal Total { get; set; }

            Add more 2 attributes

            [DisplayName("Account Number")]
            [StringLength(16)]
            public string AccountNumber { get; set; }

            [DisplayName("Security Code")]
            [StringLength(3)]
            public string SecurityCode { get; set; }

        }
    }


}