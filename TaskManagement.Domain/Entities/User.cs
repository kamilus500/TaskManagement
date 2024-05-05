namespace TaskManagement.Domain.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; } = default!;
        public string Login { get; set; } = default!;
        public string PasswordHashed { get; set; } = default!;

        public IEnumerable<TaskJob> TaskJobs { get; set; }
    }
}
