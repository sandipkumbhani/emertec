namespace MicroService_Template.Domain.Model
{
    public class ModelDimJson
    {
        public Guid Id { get; set; }
        public Guid CampaignId { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public Guid? DapperGuid { get; set; }
        public DateTime LastSyncDateTime { get; set; }
        public bool IsExecuted { get; set; }
        public bool IsRepeat { get; set; }
        public int CallLength { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
