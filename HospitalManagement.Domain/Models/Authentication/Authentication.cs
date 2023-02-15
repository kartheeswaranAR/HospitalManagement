using System;
namespace HospitalManagement.Domain.Models.Authentication
{
	public class Authentication
	{
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}

