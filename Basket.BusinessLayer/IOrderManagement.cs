using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.BusinessLayer
{
    public interface IOrderManagement
    {
        void CreateNewOrder(string name, string surname, DateTime date);

    }
}
