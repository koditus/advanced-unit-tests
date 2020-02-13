using System;

namespace Services.Models
{
    public class Lending
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public Person Person { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}