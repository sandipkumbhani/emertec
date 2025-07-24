using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Model
{
    public class VoiceFileExtendedJson
    {
        //public string? Guid { get; set; }
        public string? AgentUsername { get; set; }
        public string? AgentSalutation { get; set; } = "";
        public string? AgentFirstName { get; set; }
        public string? AgentLastName { get; set; }
        public string? CampaignName { get; set; }
        public string? CampaignDate { get; set; }
        public string? TeamName { get; set; } = "";
        public string? CallNumber { get; set; }
        public string? TelephoneNumber { get; set; }    
        public int? UserId { get; set; }
        public string? CallDateTime { get; set; }
        public string? CompanyId { get; set; }
        public string? CallRespondentFullPath { get; set; } = null;
        public List<Segment>? Segments { get; set; }
        public string? FileName { get; set; }
        public string? FullText { get; set; }
    }
    public class Segment
    {
        public double start { get; set; }
        public double end { get; set; }
        public string? text { get; set; }
        public List<Word>? words { get; set; }
        public string? Speaker { get; set; }
    }

    public class Word
    {
        public double start { get; set; }
        public double end { get; set; }
        public string? word { get; set; }

        public string? probability { get; set; }
    }

}
