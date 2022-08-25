namespace BackEnd.Data
{
    public interface IUserRepository
    {
        UserEntity? GetByUsernameAndPassword(string username, string password);
    }
}
