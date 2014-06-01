using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager.Domain.Abstract
{
    public interface IDataTransferObject
    {
        string message { get; set; }
        int id { get; set; }
    }
}
