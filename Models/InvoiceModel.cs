using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagementWebApp.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }

        [Display(Name = "Supplier")]
        public SupplierModel SelectedSupplier { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public int SelectedSupplierId { get; set; }

        // Broj racuna(fakture)
        [Display(Name = "Invoice number")]
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Invoice number must be between 3 and 20 characters.")]
        [RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "Invoice number can only contain letters, numbers, and dashes.")]
        public string InvoiceNum { get; set; }

        // Valuta placanja
        [Display(Name = "Payment dead line")]
        [DataType(DataType.Date)]
        public DateTime PaymentDeadline { get; set; }

        // Datum prijema robe
        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }

        // Poziv na broj
        [Display(Name = "Refference number")]
        [Required]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Reference number must be between 5 and 15 characters.")]
        [RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "Reference number can only contain letters, numbers, and dashes.")]
        public string ReferenceNumber { get; set; }

        // Iznos 
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public bool IsPaid { get; set; }
    }
}
