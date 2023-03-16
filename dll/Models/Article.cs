using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dll.Models
{
    public class Article
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(32, ErrorMessage = "Article number must be 32 characters or less!")]
        public string? ArticleNumber { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public DateTime CreatedUTC { get; set; }
    }
}
