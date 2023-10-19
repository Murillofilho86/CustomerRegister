namespace CustomerMicroService.Domain.Entities
{
    public class EntityBase
    {
        public EntityBase()
        {
            CreatedAt = DateTime.Now;
         }

        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedIn { get; set; }
    }
}
