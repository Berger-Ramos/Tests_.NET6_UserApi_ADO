using Library.Entity;
using UserApi.Utils.Inputs;

namespace UserApi.Domain.DomainInterface
{
    public interface IUserControl
    {
        void SaveUser();

        void UserIsValid(UserJsonInput userJsonInput);
    }
}
