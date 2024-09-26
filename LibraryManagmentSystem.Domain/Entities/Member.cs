using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Domain.Entities
{
    public class Member
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public DateTime DateOfMembership { get; set; }
        public ICollection<Loan> Loans { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
