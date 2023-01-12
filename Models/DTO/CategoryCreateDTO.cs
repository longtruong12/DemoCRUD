namespace CRUDApp.Models.DTO
{
    public class CategoryCreateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; } = 0;
    }
}
