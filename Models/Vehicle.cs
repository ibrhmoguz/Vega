namespace Vega.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public bool IsRegistered { get; set; }
        public string ContactName { get; set; }
        public string Contact { get; set; }
        public Make Make { get; set; }
        public int MakeId { get; set; }
        public Model Model { get; set; }
        public int ModelId { get; set; }
    }
}