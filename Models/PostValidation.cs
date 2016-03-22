using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMusic.Models
{
    [MetadataType(typeof(PostMetaData))]
    public partial class Post
    {
        public class PostMetaData
        {
            [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
            public System.DateTime PostDate { get; set; }

        }
    }
}