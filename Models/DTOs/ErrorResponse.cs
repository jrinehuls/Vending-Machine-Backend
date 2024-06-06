namespace VendingMachine.Models.DTOs
{
    public class ErrorResponse
    {
        public string ErrorMessage;
        public ErrorResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
