namespace StudentAttandanceLibrary.ModelDtos
{
    public class GroupDto
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string Teacher { get; set; }

        public string Room { get; set; }

        public int NumberSlots { get; set; }

        public int TotalStudents { get; set; }
    }
}
