namespace TestRentaCar.Buisness.Dtos.Maintenance
{
    public class GetMaintenanceDto
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public decimal TotalCost { get; set; }
        public decimal? InsuranceCoverage { get; set; }
    }
}
