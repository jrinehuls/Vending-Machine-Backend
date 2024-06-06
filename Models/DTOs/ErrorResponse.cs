namespace VendingMachine.Models.DTOs
{
    public class ErrorResponse
    {

        public ErrorResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; private set; }
    }
}
