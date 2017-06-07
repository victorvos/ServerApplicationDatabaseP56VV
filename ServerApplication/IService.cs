using ServerApplication.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServerApplication
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        bool Login(string username, string password);

        [OperationContract]
        string RegistreerUser(string username);

        [OperationContract]
        List<ProductDto> GetAvailableProducts();

        [OperationContract]
        List<OrderDto> GetInventory(string username);
    }
}
