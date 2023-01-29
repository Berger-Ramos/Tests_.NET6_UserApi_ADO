using Library.Entity;
using UserApi.Utils;
using Library.RepositoryInterface;
using UserApi.Utils.Inputs;
using UserApi.Domain.DomainInterface;

namespace UserApi.Domain
{
    public class UserControl : IUserControl
    {
        private static IUserRepository UserRepository { get; set; }
        private static User User { get; set; }
        public UserControl(User user)
        {
            User = user;
        }

        public void SaveUser()
        {
            using (UserRepository = new Library.EFRepository.UserDAO())
            {
                if (UserRepository.GetUserByName(User.Name) != null)
                    throw new Exception("usuário já castrado");

                bool CreatedUser = UserRepository.Save(User);

                if (!CreatedUser)
                    throw new Exception("não foi possível cadastrar esse usuário");

                UserRepository.Commit();
            }
        }

        public void UserIsValid(UserJsonInput userJsonInput)
        {
            UserRepository = new Library.EFRepository.UserDAO();
            User user = UserRepository.GetUser(userJsonInput.Name, userJsonInput.Password);

            if (user == null|| user.IsAdmin == false)
                throw new Exception(UserMSG.EXC0001);
        }
    }
}
