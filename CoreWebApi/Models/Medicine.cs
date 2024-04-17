using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebApi.Models
{
    public class Medicine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Amount {  get; set; }
        public string Origin {  get; set; }
        [ForeignKey("Unit")]
        public int UnitID {  get; set; }
        [ValidateNever]
        public Unit Unit { get; set; }
    }
}
