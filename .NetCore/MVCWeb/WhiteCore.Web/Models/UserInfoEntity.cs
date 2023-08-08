using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteCore.Web.Models
{
    [Table("UserInfo")]
    [Serializable]
    public class UserInfoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Age")]
        public int Age { get; set; }

        [Column("Mobile")]
        public string Mobile { get; set; }

        [Column("AddTime")]
        public DateTime AddTime { get; set; }

        [Column("Type")]
        public int Type { get; set; }
    }
}
