using System.ComponentModel.DataAnnotations;

namespace ILoveEnglishSchool.Models {
    public class PartModules {
        [Key]
        public int ModuleId { get; set; }
        public Part Part { get; set; }
        public int PartId { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string ContentUrl { get; set; }
    }
}