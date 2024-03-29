﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Model.DTO
{
    public class CreateRegionDto
    {
        [Required]
        [MinLength(3,ErrorMessage ="Code has to be a minimum of 3 characters")]
        [MaxLength(10,ErrorMessage ="Code has to be a minimum of 10 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Code has to be a minimum of 100 characters")]
        public string Name { get; set; }
       
        public string? RegionImageUrl { get; set; }

    }
}
