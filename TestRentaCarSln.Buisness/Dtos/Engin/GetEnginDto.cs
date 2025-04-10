namespace TestRentaCar.Buisness.Dtos.Engin
{
    public class GetEnginDto
    {
        public int Id { get; set; }
        public string EnginType { get; set; }  //benzin or diesel?
        public decimal EnginCapacity { get; set; } // 4.4, 2.0
    }
}
