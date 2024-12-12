using AlgarTech.Test.Data.Models;

namespace AlgarTech.Test.Dto
{
    public class OrdersDto
    {
        public string ClientIdentification { get; set; }
        public string ClientAddress { get; set; }
        public decimal Total { get; set; }
        public List<Products> Products { get; set; }
    }
}
