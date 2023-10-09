using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDirectory.Data.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Display(Name = "Отчество")]
        public string Midlname { get; set; }
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Display(Name = "")]
        public string UrlPhoto { get; set; }
        [ForeignKey("SectionId")]
        [Display(Name = "Отделы")]
        public Section Section { get; set; }
        [NotMapped]
        [Display(Name = "Фото")]
        public IFormFile PhotoForm { get; set; }
    }
}