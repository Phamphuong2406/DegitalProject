﻿using System.ComponentModel.DataAnnotations;

namespace DigitalProject.Models.Project
{
  
        public class ProjectDTO
        {
            public int? ProjectId { get; set; }
        [Required(ErrorMessage = "Bạn chưa điền tên dự án")]
        [StringLength(50, ErrorMessage = "Tên dự án không vượt quá 50 ký tự")]
        public string? ProjectName { get; set; }
            public string? ProjectType { get; set; }
            // public string avataUrl { get; set; }
            public string? ImageUrl { get; set; }
            public string? Shortdescription { get; set; }
            public string? DetailedDescription { get; set; }
            public string? architect { get; set; }
            public string? structuralEngineer { get; set; }
            public DateTime ConstructionStartTime { get; set; }
            public DateTime ConstructionEndTime { get; set; }
            public DateTime PostedTime { get; set; }
            public bool DisplayOnhome { get; set; }
            public int? DisplayOrderOnHome { get; set; }
            public bool DisplayOnHeader { get; set; }
            public int? DisplayOrderOnHeader { get; set; }
            public DateTime ExpirationTimeOnHeader { get; set; }
            public int IdPoster { get; set; }
        }
    }
