using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kztek_Model.Models
{
    [Table("CDKey")]
    public class CDKey
    {
        [Key]
        public string Id { get; set; }
        public string Code { get; set; }
        public string AppId { get; set; }//key phần mềm nào
        public string UserCreated { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsExpire { get; set; }
    }
}