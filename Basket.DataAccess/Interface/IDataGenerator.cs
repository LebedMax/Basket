using System.Collections.Generic;
using Basket.Models;

namespace Basket.DataAccess.Interface
{

    public interface IDataGenerator
    {
        List<Product> GenerateProducts(int count);

        List<Buyer> GenerateBuyers(int count);
    }
}