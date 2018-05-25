using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vega.Models
{
    [Table("Vehicle")]
    public class Vehicle
    {
        public int Id { get; set; }
        public bool IsRegistered { get; set; }
        public Model Model { get; set; }
        public int ModelId { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactMail { get; set; }
        public DateTime LastUpdate { get; set; }

        public ICollection<VehicleFeature> Features { get; set; }
    }
}