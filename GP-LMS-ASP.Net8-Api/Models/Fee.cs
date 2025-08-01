﻿namespace GP_LMS_ASP.Net8_Api.Models
{
    public enum FeeType
    {
        Monthly = 1,
        Books = 2,
        All = 3
    }

    public enum FeeStatus
    {
        Paid = 1,
        Unpaid = 2,
        Overdue = 3
    }

    public class Fee
    {
        public int FeeId { get; set; }

        public int StudentId { get; set; }
        public virtual User Student { get; set; }

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public FeeType Type { get; set; }   // Monthly, Books
        public FeeStatus Status { get; set; } // Paid, Unpaid

        public string Notes { get; set; }
        public decimal? Discount { get; set; }
        public decimal NetAmount { get; set; }

        public int GroupId { get; set; }
        public virtual Groups Group { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public int PaymentCycleId { get; set; }

        public string UpdatedByUser { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}