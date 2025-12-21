

using System.ComponentModel.DataAnnotations.Schema;

namespace HybridCachingDemo.Models
{
    public class Product
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("brand_id")]
        public int BrandId { get; set; }
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Column("model_year")]

        public int ModelYear { get; set;}
        [Column("list_price")]
        public decimal ListPrice { get; set; }
    }
}
