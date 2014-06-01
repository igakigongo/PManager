using PManager.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager.Domain.Concrete
{
    public class JsonDTO: IDataTransferObject
    {
        public string message { get; set; }

        public int id { get; set; }
    }
}
