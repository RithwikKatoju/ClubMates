﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClubMates.Web.Models.AdminViewModel
{
    public class ClubViewModel
    {

        public int ClubId { get; set; }

        [Required(ErrorMessage ="CLubNAme is mandatory")]
        public string? ClubName { get; set; }
        public string? ClubDescription { get; set; }
        public ClubCategory CLUBCATEGORY { get; set; }
        public ClubType CLUBTYPE { get; set; }
        public string? ClubRules { get; set; }
        public string? ClubManager { get; set; }
        public string? ClubContactNumber { get; set; }
        public string? ClubEmail { get; set; }

        public List<SelectListItem>? ClubCategories
        {
            get
            {
                List<SelectListItem> selectListItems = Enum.GetValues<ClubCategory>().Select(x => new SelectListItem
                {
                    Text = Enum.GetName(x),
                    Value = x.ToString()
                }).ToList();
                return selectListItems;
            }
        }

        public List<SelectListItem>? ClubTypes
        {
            get
            {
                List<SelectListItem> selectListItems = Enum.GetValues<ClubType>().Select(x => new SelectListItem
                {
                    Text = Enum.GetName(x),
                    Value = x.ToString()
                }).ToList();
                return selectListItems;
            }
        }


    }
    
}
