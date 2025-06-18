namespace MicroService_Template.Domain.Model
{
    public class ModelDimTextSentence
    {
        public Guid Id { get; set; }
        public Guid JsonGuid { get; set; }
        public string? Sentence { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Speaker { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
