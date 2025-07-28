using GP_LMS_ASP.Net8_Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Expense
{
    public int ExpenseId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; }
    public string PaidBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
}