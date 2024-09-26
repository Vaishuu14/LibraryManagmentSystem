using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Domain.Entities
{
    public class Book
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int NumberOfCopies { get; set; }

        public ICollection<Loan> Loans { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
