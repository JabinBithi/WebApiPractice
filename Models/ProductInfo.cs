using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }

        public string? RollNo { get; set; }

        public decimal? GSM { get; set; }

        public decimal? Width { get; set; }

        public string? ColorFastness { get; set; }

        public string? PillingTest { get; set; }

        public string? Twist { get; set; }
    }
}
 
