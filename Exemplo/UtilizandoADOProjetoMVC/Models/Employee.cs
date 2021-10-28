using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UtilizandoADOProjetoMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="O campo é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório")]
        public string Position { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório")]
        public int Age { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório")]
        public string Office { get; set; }
    }
}
