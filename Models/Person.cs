using System;

namespace Restfulangularcore.Models
{
    public class Person
    {
        public Guid? Document_Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; } 
    }
}