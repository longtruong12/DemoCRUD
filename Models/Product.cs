namespace CRUDApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Descreption { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
