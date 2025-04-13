namespace ChatApp.Users
{
    public static class UserStore
    {
        // Simple in-memory user store for demonstration purposes
        public static Dictionary<string, string> Users = new()
        {
            { "admin", "admin123" },
            { "guest", "guest123" }
        };
    }
}
