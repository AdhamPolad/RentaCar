namespace TestRentaCar.Buisness.Dtos.Maintenance
{
    public class UpdateMaintenanceDto
    {
        public int Id { get; set; }                 
        public DateTime MaintenanceDate { get; set; }
        public decimal TotalCost { get; set; }
        public decimal? InsuranceCoverage { get; set; }
    }
}
