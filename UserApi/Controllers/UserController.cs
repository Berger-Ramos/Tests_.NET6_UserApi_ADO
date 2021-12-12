using Library.Entity;
using Library.Repository;
using Library.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using UserApi.Domain;
using UserApi.Domain;

namespace UserApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SaveUser([FromBody] User user)
        {
            try
            {
                UserControl userControl = new UserControl(user);

                userControl.UserIsValid();
                userControl.SaveUser();

                return Created("",new { Success = true, Response = user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Response = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult GetUserByName([FromBody] User JsonUser)
        {

            IUserRepository repository = new Library.EFRepository.UserDAO();

            User user = repository.GetUserByName(JsonUser.Name);

            repository.Dispose();

            return Ok(new { Success = true, Response = user });
        }

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
