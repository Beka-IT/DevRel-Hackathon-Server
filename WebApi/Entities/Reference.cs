namespace WebApi.Entities
{
    public class Reference
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
    }
}
