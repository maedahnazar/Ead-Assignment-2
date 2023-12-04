using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Core.Models;

public class SubjectRequestDto : ISubject
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
}
