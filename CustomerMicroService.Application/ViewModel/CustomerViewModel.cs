﻿namespace CustomerMicroService.Application.ViewModel
{
    public class CustomerViewModel
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public AddressViewModel Address { get; set; }
    }
}
