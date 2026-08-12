using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWithFluxor.models.Cars
{
    public class CarDetailModel
    {
        public int Id { get; set; }
        public string CarName { get; set; } = "";
        public string Car_Variant { get; set; } = "";
    }
}