﻿using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Producer : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        //
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ProfilePictureURL { get; set; }
        //
		[Display(Name = "Full Name")]
		[Required(ErrorMessage = "Full name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 50 characters")]
		public string FullName { get; set; }
        //
		[Display(Name = "Biography")]
		[Required(ErrorMessage = "Biography is required")]
		public string Bio {  get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }

    }
}