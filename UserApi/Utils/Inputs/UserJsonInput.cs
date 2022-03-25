using System.ComponentModel.DataAnnotations;

namespace UserApi.Utils.Inputs
{
    public class UserJsonInput
    {
        [Required( ErrorMessageResourceType = typeof(UserMSG), ErrorMessageResourceName = "EXC0002")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(UserMSG), ErrorMessageResourceName = "EXC0003")]
        public string Password  { get; set; }

        public bool IsAdmin { get; set; }
    }
}
