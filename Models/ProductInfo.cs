using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }
        public string RollNo { get; set; } = string.Empty;

        public string GSM { get; set; } = string.Empty;

        public string Width { get; set; } = string.Empty; 
        public string ColorFastness { get; set; } = string.Empty;

        public string PillingTest { get; set; } = string.Empty;
        public string Twist { get; set; } = string.Empty;
         
    }
}
