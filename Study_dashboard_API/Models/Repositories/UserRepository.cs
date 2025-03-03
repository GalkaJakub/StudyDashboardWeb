namespace Study_dashboard_API.Models.Repositories
{
    public static class UserRepository
    {
        private static List<User> users = new List<User>()
        {
                new User { UserId = 0, Name = "Kuba1", Password = "kuba1111" },
                new User { UserId = 1, Name = "Kuba2", Password = "kuba2222" },
                new User { UserId = 2, Name = "Kuba3", Password = "kuba3333" },
                new User { UserId = 3, Name = "Kuba4", Password = "kuba4444" },
                new User { UserId = 4, Name = "Kuba5", Password = "kuba5555" },
                new User { UserId = 5, Name = "Kuba6", Password = "kuba6666" }
    };

        public static bool userExists(int id)
        {
            return users.Any(user => user.UserId == id);
        }

        public static User? getUserById(int id)
        {
            return users.FirstOrDefault(x => x.UserId == id);
        }

        public static List<User> getUsers()
        {
            return users;
        }

        public static User? getUserByName(string name)
        {
            return users.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public static void addUser(User user)
        {
            int newId = users.Max(x => x.UserId) +1;
            user.UserId = newId;
            users.Add(user);
        }

        public static void updateUser(User user)
        {
           var oldUser = users.First(x => x.UserId == user.UserId);
           oldUser.Name = user.Name;
           oldUser.Email = user.Email;
           oldUser.Password = user.Password;
        }

        public static void deleteUser(int id)
        {
            var user = getUserById(id);
            if (user != null)
                users.Remove(user);
        }
    }
}
