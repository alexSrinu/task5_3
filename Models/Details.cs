﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace task5.Models
{
    public class Details
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^[89]\d{9}$", ErrorMessage = "The mobile number must be 10 digits long and start with 8 or 9.")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public int HobbyId { get; set; }
        public string HobbyName { get; set; }
        public City city { get; set; }
        public State state { get; set; }
        public int pagesize { get; set; }
        public int pagenumber { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public class PersonHobbies
        {
            public int Id { get; set; }
            public int HobbyId { get; set; }
        }
      //  public List<int> SelectedHobbies { get; set; } = new List<int>();

        // public List<string> SelectedHobbies { get; set; } = new List<string>();
        public class Hobby
        {
            public int HobbyId { get; set; }
            public string HobbyName { get; set; }
            public bool IsChecked { get; set; } // New property
        }
        public List<Hobby> Hobbies { get; set; } = new List<Hobby>();
        public List<int> SelectedHobbies { get; set; } = new List<int>();
        public class DynamicViewModel
        {
            public List<Details> DetailsList { get; set; }
        }

        public class StateCityGroup
        {
            public string StateName { get; set; }
            public List<string> Cities { get; set; }
        }
        public MyClass MyClassInstance { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public class PaginatedResult
        {
            public IEnumerable<Details> Det { get; set; }
            public int TotalPages { get; set; }
            public int PageSize { get; set; }
            public int TotalCount { get; set; }
            public int CurrentPage { get; set; }
        }

    }
}