namespace MicroService_Template.Domain.Model
{
    public class ModelDimCampaign
    {
        public Guid Id { get; set; }
        public Guid? CompanyId { get; set; }
        public String? Name { get; set; }
        public String? Description { get; set; }
        public String? Campaignpath { get; set;}
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
