using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseManagement.Models
{
    [Table("Cases")]
    public class Case
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CaseId { get; set; }

        [Required]
        [StringLength(50)]
        public string CaseNumber { get; set; }

        [Required]
        [StringLength(500)]
        public string CaseTitle { get; set; }

        [Required]
        public string CaseDescription { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // Pending, InReview, Approved, Rejected, Overdue

        [Required]
        public int MakerUserId { get; set; }

        public int? CheckerUserId { get; set; }

        [StringLength(500)]
        public string MakerNotes { get; set; }

        [StringLength(500)]
        public string CheckerNotes { get; set; }

        public DateTime? CheckedDate { get; set; }

        [StringLength(50)]
        public string Priority { get; set; } // Low, Medium, High, Urgent

        [StringLength(500)]
        public string DocumentPath { get; set; }

        public bool IsOverdue
        {
            get { return DueDate < DateTime.Now && Status != "Approved" && Status != "Rejected"; }
        }

        public bool OverdueNotificationSent { get; set; }

        [ForeignKey("MakerUserId")]
        public virtual User MakerUser { get; set; }

        [ForeignKey("CheckerUserId")]
        public virtual User CheckerUser { get; set; }
    }
}
