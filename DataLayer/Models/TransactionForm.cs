using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PRN221_Project.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRN221_Project.Models
{
    public class TransactionForm
    {
        public int TransactionId { get; set; }
        public int CategoryId { get; set; }
        public int AccountId { get; set; }
        public int Amount { get; set; }
        public string? Note { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
