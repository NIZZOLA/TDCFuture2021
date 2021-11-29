using LocationMinApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationMinApi.Contracts
{
    public class PlaceHierarchyResultModel
    {
        public PlaceHierarchyResultModel()
        {
            this.Hierarchy = new List<LocationModel>();
        }
        public PlaceModel Place { get; set; }
        public ICollection<LocationModel> Hierarchy { get; set; }
    }
}
