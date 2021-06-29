using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kztek_Model.Models
{
    [Table("Project")]
    public class Project
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
