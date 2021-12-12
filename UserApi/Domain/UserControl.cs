using Library.Entity;
using UserApi.Utils;
using Library.RepositoryInterface;

namespace UserApi.Domain
{
    public class UserControl
    {
        public static IRepository<User> UserRepository { get; set; }

        private static User User { get; set; }
        public UserControl(User user)
        {
            User = user;
        }

        public void SaveUser()
        {
            //TransactionDB transaction = new TransactionDB();

            UserRepository = new Library.EFRepository.UserDAO();
            UserRepository.Save(User);

            //transaction.Commit();
        }

        public void UserIsValid()
        {
            if (User == null)
                throw new InvalidOperationException(UserMSG.EXC0001);
        }

    }
}
