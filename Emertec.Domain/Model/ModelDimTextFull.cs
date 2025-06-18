namespace MicroService_Template.Domain.Model
{
    public class ModelDimTextFull
    {
        public Guid? Id { get; set; }
        public Guid? JsonGuid { get; set; }
        public string? name { get; set; }
        public string? campaign_date { get; set; }
        public string? FullText { get; set; }
        public string? Size { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
