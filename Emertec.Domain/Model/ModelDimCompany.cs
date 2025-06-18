namespace MicroService_Template.Domain.Model
{
    public class ModelDimCompany
    {
        public Guid Id {get; set;}
        public String? Name {get; set;}
        public String? Description {get; set;}
        public DateTime? Created {get; set;}
        public DateTime? Modified {get; set;}
    }
}
