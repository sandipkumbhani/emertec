using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicroService_Template.Domain.Model
{
    public class ModelDimJson
    {
        public Guid Id { get; set; }

        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public long UserId { get; set; }
        public string? TelephoneNumber { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "bigint")]
        public long InsertBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime InsertDate { get; set; }
        [Column(TypeName = "bigint")]
        public long UpdateBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime UpdateDate { get; set; }
        [ForeignKey("UserId")]
        public virtual ModelUsers? ModelUsers { get; set; }
        [NotMapped]
        public List<ModelSentence> Sentences { get; set; } = new();
        public class ModelSentence
        {
            public string Text { get; set; } = string.Empty;
        }

        //public Guid CampaignId { get; set; }
        //public Guid? DapperGuid { get; set; }
        //public DateTime LastSyncDateTime { get; set; }
        //public bool IsExecuted { get; set; }
        //public bool IsRepeat { get; set; }
        //public int CallLength { get; set; }
        //public DateTime? Created { get; set; }
        //public DateTime? Modified { get; set; }
    }
}
