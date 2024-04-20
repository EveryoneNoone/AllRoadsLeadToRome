namespace Core.Entities
{
    public class UserSession : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
