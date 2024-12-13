﻿namespace libAPI.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime MembershipDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}