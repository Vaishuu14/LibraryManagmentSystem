using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public DateTime ReservedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
