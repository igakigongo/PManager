namespace PManager.WebUI.DTOS
{
    public class ReportDto
    {
        public int ReportId { get; set; }

        public string ReportCode { get; set; }

        public int IncompleteTasks { get; set; }

        public int CompletedTasks { get; set; }
    }
}