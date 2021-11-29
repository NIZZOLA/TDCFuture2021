using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace LocationMinApi.Models
{
    [Table("Location")]
    public class LocationModel
    {
        [Key]
        public int LocationId { get; set; }

        [MaxLength(60)]
        public string LocationName { get; set; }

        [MaxLength(5)]
        public string LocationCode { get; set; }

        public LocationTypeEnum LocationType { get; set; }

        [NotMapped]
        public string PolygonString { get; set; }

        [Column(TypeName = "decimal(15, 9)")]
        public double Latitude { get; set; }
        [Column(TypeName = "decimal(15, 9)")]
        public double Longitude { get; set; }
    }
}
