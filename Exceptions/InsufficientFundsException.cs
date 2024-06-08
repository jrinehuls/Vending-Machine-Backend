using VendingMachine.Models.Entities;

namespace VendingMachine.Exceptions
{
    public class InsufficientFundsException : ApiException
    {
        public InsufficientFundsException(double providedFunds, double cost) :
            base ($"You only provided ${providedFunds:0.00} for a snack that costs ${cost:0.00}.") 
        {
            this.statusCode = 400;
        }
    }
}
