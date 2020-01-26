using System.ComponentModel.DataAnnotations;

namespace server.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Телефон обязателен для заполнения")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        public string Password { get; set; }
    }
}
