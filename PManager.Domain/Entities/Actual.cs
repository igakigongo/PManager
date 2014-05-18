using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager.Domain.Entities
{
    public class Actual
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Decimal Budget { get; set; }
    }
}
