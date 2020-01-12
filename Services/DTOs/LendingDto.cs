using System;

namespace Services.DTOs
{
    public class LendingDto : IDto
    {
        public int Id { get; set; }
        public BookDto Book { get; set; }
        public PersonDto Person { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}