﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudyDS_web.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int? Ects { get; set; }
        [Required]
        [Range(0, 2)]
        public PriorityLevelEnum PriorityLevel { get; set; }
        public int UserId { get; set; }
    }
}
