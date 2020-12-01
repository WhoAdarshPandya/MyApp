using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyApp.Models
{
    public class PlacesModel
    {
        public int id { get; set; }

        [Required(ErrorMessage ="name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "description is required")]

        public string description { get; set; }
        [Required(ErrorMessage = "prime is required")]

        public string isPrime { get; set; }
        [Required(ErrorMessage = "image url is required")]

        public string image_url { get; set; }
        [Required(ErrorMessage = "price is required")]

        public string price { get; set; }
        [Required(ErrorMessage = "days is required")]

        public string days { get; set; }
    }
}