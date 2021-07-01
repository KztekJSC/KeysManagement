using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kztek_Model.Models
{
    [Table("ActiveKey")]
    public class ActiveKey
    {
        [Key]
        public string Id { get; set; }
        public string CDKey { get; set; }
        public string KeyActive { get; set; }
        public string AppId { get; set; }//key phần mềm nào
        public string UserCode { get; set; }
        public string CustomerId { get; set; }
        public string ProjectId { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
    }
}