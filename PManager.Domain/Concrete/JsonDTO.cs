using PManager.Domain.Abstract;

namespace PManager.Domain.Concrete
{
    public class JsonDTO: IDataTransferObject
    {
        public string message { get; set; }

        public int id { get; set; }
    }
}
