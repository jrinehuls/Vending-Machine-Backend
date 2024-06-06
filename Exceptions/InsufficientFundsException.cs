using VendingMachine.Models.Entities;

namespace VendingMachine.Exceptions
{
    public class InsufficientFundsException : ApiException
    {
        public InsufficientFundsException(double providedFunds, Snack snack) :
            base ($"You only provided ${providedFunds} money for a snack that costs ${snack.Cost}.") 
        {
            this.statusCode = 400;
        }
    }
}
