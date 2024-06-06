namespace VendingMachine.Exceptions
{
    public class SoldOutException : ApiException
    {
        public SoldOutException() : base("This product is sold out")
        {
            this.statusCode = 422;
        }
    }
}
