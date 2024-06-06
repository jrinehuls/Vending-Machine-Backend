namespace VendingMachine.Exceptions
{
    public class ApiException : Exception
    {
        protected int statusCode;
        public ApiException(string message) : base(message) { }
        public int StatusCode { get { return statusCode; } }
    }
}
