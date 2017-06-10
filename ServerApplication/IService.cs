using ServerApplication.Database;
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

        [OperationContract]
        void BuyProduct(long user_Id, long product_Id, int amount);

        [OperationContract]
        UserDto GetBalance(string username);

        [OperationContract]
        UserDto GetUserID(string username);

        [OperationContract]
        ProductDto GetProductID(string productName);

        [OperationContract]
        Product GetProduct(long productID);
    }
}
