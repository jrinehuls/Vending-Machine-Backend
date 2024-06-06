using VendingMachine.Models.Entities;

namespace VendingMachine.Exceptions
{
    public class InsufficientFundsException : ApiException
    {
        public InsufficientFundsException(double providedFunds, double cost) :
            base ($"You only provided ${providedFunds} for a snack that costs ${cost}.") 
        {
            this.statusCode = 400;
        }
    }
}
