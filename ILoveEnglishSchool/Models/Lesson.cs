using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ILoveEnglishSchool.Models {
    public class Lesson {
        [Key]
        public int LessonId { get; set; }
        public Level Level { get; set; } = Level.None;
        public int LessonNumber { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}