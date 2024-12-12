namespace AlgarTech.Test.Data.Models
{
    public class Orders
    {
        public int IDOrder { get; set; }
        public string ClientIdentification { get; set; }
        public string ClientAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
    }
}
