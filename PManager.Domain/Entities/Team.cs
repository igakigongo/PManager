﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager.Domain.Entities
{
    public class Team
    {
        [Key]
        [ForeignKey("Project")]
        public int Id { get; set; }

        [Required(ErrorMessage="* Required")]
        public string Name { get; set; }

        [Required]
        public Project Project { get; set; }

        public List<User> Users { get; set; }
    }
}
