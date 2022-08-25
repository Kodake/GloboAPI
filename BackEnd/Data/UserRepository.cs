using BackEnd.Helpers;

namespace BackEnd.Data
{
    public class UserRepository: IUserRepository
    {
        private readonly List<UserEntity> users = new()
        {
            new UserEntity(3522, "kodake", "12345", "blue", "Admin")
        };

        public UserEntity? GetByUsernameAndPassword(string username, string password)
        {
            var user = users.SingleOrDefault(u => u.Name == username && u.Password == password);
            return user;
        }
    }
}
