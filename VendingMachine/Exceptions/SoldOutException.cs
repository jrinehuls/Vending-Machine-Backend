using VendingMachine.Models.Entities;

namespace VendingMachine.Exceptions
{
    public class SoldOutException<T> : ApiException where T : Snack
    {
        public SoldOutException(T item) : base($"{item.Name} is sold out")
        {
            this.statusCode = 422;
        }
    }
}
