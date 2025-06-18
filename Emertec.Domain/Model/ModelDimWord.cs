namespace MicroService_Template.Domain.Model
{
    public class ModelDimWord
    {
            public Guid Id { get; set; }
            public Guid JsonId { get; set; }                
            public Guid TextSentenceId { get; set; }
            public String? Word { get; set; }
            public String? OriginalWord { get; set; }
            public String? SoundsLike { get; set; }
            public String? Fuzzy { get; set; }
            public int RateProfanity { get; set; }
            public int RateComplexity { get; set;}
            public String? VoicePrint { get; set; }
            public double StartTime { get; set; } = 0;          
            public double EndTime { get; set; } = 0;
            public string? Speaker { get; set;}
            public double Probability { get; set; } = 0;
            public DateTime? Created { get; set; }
            public DateTime? Modified { get; set; }
    }
}
