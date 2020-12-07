using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ILoveEnglishSchool.Models {
    public class Part {
        [Key]
        public int PartId { get; set; }
        public string WelcomeImage { get; set; }
        public string Name { get; set; }
        public Lesson Lesson { get; set; }
        public int LessonId { get; set; }
        public LessonType Type { get; set;}
        public ICollection<PartModules> Modules { get; set; } = new List<PartModules>();
    }
}