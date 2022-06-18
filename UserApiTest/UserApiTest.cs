using Microsoft.AspNetCore.Mvc;
using UserApi.Controllers;
using UserApi.Utils.Inputs;
using Xunit;

namespace UserApiTest;

public class UserApiTest
{
    UserController _UserController;

    public UserApiTest()
    {
        _UserController = new UserController();
    }


    [Fact]
    public void GetUserByName_GetUser_SuccessOk()
    {
        string UserNameTest = "Admin";

        var okResult = _UserController.GetUserByName(UserNameTest);

        // Assert
        Assert.IsType<OkObjectResult>(okResult);
    }

    [Fact]
    public void GetUsers_GetUsers_SuccessOk()
    {
        var okResult = _UserController.GetUsers();

        // Assert
        Assert.IsType<OkObjectResult>(okResult);
    }

    [Fact]
    public void SaveUser_SaveNewUser_SuccessOk()
    {
        string ramdomName = UserApiTestUtil.RamdoAlphanumeric();

        UserJsonInput JsonSaveUser = new UserJsonInput()
        {
            IsAdmin = true,
            Name = string.Format("TestUser {0}", ramdomName),
            Password = $"TestUserPassWord {ramdomName}"
        };

        var okResult = _UserController.SaveUser(JsonSaveUser);

        // Assert
        Assert.IsType<OkObjectResult>(okResult);
    }
}