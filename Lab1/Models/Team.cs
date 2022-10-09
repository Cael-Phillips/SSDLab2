using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1.Models {
    public class Team{
        //ID
        [Required]
        [Display(Name = "Team ID")]
        public int TeamID { get; set; }
        //Team Name
        [Required]
        [Display(Name = "Team Name")]
        public string? TeamName { get; set; }
        //Email
        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        //Established Date
        [Display(Name = "Date Established")]
        public DateTime EstablishedDate { get; set; }
    }//class
}//namespace
