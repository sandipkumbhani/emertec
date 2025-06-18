namespace MicroService_Template.Domain.Model
{
    public class ModelDimTeam
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? path { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

    }
}
