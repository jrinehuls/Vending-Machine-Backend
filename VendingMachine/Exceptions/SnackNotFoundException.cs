namespace VendingMachine.Exceptions
{
    public class SnackNotFoundException : ApiException
    {

        public SnackNotFoundException(long id) :
            base($"Snack with id {id} not found") 
        {
            this.statusCode = 404;
        }

    }
}
