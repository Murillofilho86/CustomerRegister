﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerMicroService.Application.ViewModel
{
    public class AddressViewModel
    {

        public string Street { get;  set; }

        public string Number { get;  set; }

        public string Complement { get; set; }
        
        public string Neighborhood { get;  set; }

        public string City { get;  set; }

        public string State { get; set; }

        public string Zipcode { get;  set; }
    }
}
