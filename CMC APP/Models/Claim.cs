using Microsoft.AspNetCore.Http;
using System;

namespace CMCS.Models
{
    //creating class
    public class Claim
    {
        public int Id { get; set; }
        public string LecturerName { get; set; }
        public string Email { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalAmount { get; set; }
        public string SupportingDocumentPath { get; set; }
        public IFormFile SupportingDocument { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
