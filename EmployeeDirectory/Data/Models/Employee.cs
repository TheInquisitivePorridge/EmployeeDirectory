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
        [Display(Name = "���")]
        public string Name { get; set; }
        [Display(Name = "�������")]
        public string Surname { get; set; }
        [Display(Name = "��������")]
        public string Midlname { get; set; }
        [Display(Name = "����� ��������")]
        public string PhoneNumber { get; set; }
        [Display(Name = "")]
        public string UrlPhoto { get; set; }
        [ForeignKey("SectionId")]
        [Display(Name = "������")]
        public Section Section { get; set; }
        [NotMapped]
        [Display(Name = "����")]
        public IFormFile PhotoForm { get; set; }
    }
}