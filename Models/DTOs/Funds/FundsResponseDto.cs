namespace VendingMachine.Models.DTOs.Funds
{
    public class FundsResponseDto
    {
        public double TotalChange { get; set; }
        public int Fives { get; set; }
        public int Ones { get; set; }
        public int Quarters { get; set; }
        public int Dimes { get; set; }
        public int Nickles { get; set; }
        public int Pennies { get; set; }

    }
}
