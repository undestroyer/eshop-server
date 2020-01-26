using System.ComponentModel.DataAnnotations;

namespace server.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Номер телефона должен быть заполнен")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Пароль должен быть заполнен")]
        public string Password { get; set; }
    }
}
