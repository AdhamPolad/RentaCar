﻿namespace TestRentaCar.Buisness.Dtos.Customer
{
    public class UpdateCustomerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string DriverLisenceNumber { get; set; }
    }
}
