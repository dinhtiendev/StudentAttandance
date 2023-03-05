namespace StudentAttandanceLibrary.ModelDtos
{
    public class SessionDto
    {
        public int SessionId { get; set; }

        public string? GroupName { get; set; }

        public int TimeSlotId { get; set; }

        public string? Description { get; set; }

        public string? RoomName { get; set; }

        public string? TeacherId { get; set; }

        public DateTime Date { get; set; }

        public string? DayOfWeek { get; set; }

        public int Index { get; set; }

        public bool Attanded { get; set; }

        //public bool Present { get; set; }

        //public string? Comment { get; set; }
    }
}
