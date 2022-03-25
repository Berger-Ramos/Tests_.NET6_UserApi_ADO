using Library.Entity;
using Library.RepositoryInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi.Domain;
using UserApi.Utils.Inputs;

namespace UserApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public IActionResult SaveUser([FromBody] UserJsonInput userJsonInput)
        {
            try
            {
                User user = new User
                {
                    Name = userJsonInput.Name,
                    Password = userJsonInput.Password,
                    IsAdmin = userJsonInput.IsAdmin
                };

                UserControl userControl = new UserControl(user);

                userControl.SaveUser();

                return Ok(new { Success = true, Response = user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Response = ex.Message });
            }
        }
        
        [HttpPost]
        public IActionResult GetUserByName([FromQuery] string Name)
        {

            IUserRepository repository = new Library.EFRepository.UserDAO();

            User user = repository.GetUserByName(Name);
            repository.Dispose();

            return Ok(new { Success = true, Response = user });
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUsers()
        {
            Library.EFRepository.UserDAO repository = new Library.EFRepository.UserDAO();

            List<User> users = repository.GetUsers();
            repository.Dispose();

            return Ok(users);
        }
    }
}
