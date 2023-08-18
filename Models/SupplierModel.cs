using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagementWebApp.Models
{
    public class SupplierModel
    {
        public int Id { get; set; }

        [Display(Name = "Supplier name")]
        [Required]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Supplier name must be between 2 and 40 characters.")]
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "Only letters and numbers are allowed.")]
        public string SupplierName { get; set; }

        [Display(Name = "Supplier Adress")]
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Supplier address must be between 3 and 50 characters.")]
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "Only letters and numbers are allowed.")]
        public string SupplierAdress { get; set; }

        [Display(Name = "Bank account number")]
        [RegularExpression(@"^\d{3}-\d{13}-\d{2}$", ErrorMessage = "Bank account number must be in the format XXX-ZZZZZZZZZZZZZ-YY.")]
        [StringLength(20, MinimumLength = 20, ErrorMessage = "Bank account number must be 20 characters.")]
        [Required]
        public string BankAccountNumber { get; set; }
    }
}