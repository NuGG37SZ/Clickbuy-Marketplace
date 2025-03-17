namespace AdminService.Model.Entity
{
    public class Report
    {
        public int Id { get; set; } 

        public int UserId { get; set; }

        public int CategoryReportId { get; set; }

        public string Subject { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }
    }
}
