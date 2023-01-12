namespace CRUDApp.Models.DTO
{
    public class ProductCreateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Descreption { get; set; }

        public int CategoryId { get; set; }
    }
}
