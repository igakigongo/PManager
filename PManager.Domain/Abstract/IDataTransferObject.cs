namespace PManager.Domain.Abstract
{
    public interface IDataTransferObject
    {
        string message { get; set; }
        int id { get; set; }
    }
}
