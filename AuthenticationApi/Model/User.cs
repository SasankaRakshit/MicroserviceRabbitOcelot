namespace AuthenticationApi.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsActive { get; set; } = 1;
        public DateTime Created { get; set; } = DateTime.Now;
        public string Phone { get; set; }
        public string Role { get; set; }
    }
}
