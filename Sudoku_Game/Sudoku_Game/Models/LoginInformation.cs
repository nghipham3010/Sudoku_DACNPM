namespace Sudoku_Game.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoginInformation")]
    public partial class LoginInformation
    {
        [Key]
        [StringLength(50)]
        public string TK { get; set; }

        [StringLength(64)]
        public string MK { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Quyen { get; set; }

        public int? Score { get; set; }

        public int? FailedAttempts { get; set; }

        public DateTime? LockoutEnd { get; set; }
    }
}
