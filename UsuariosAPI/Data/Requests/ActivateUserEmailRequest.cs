using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Requests
{
    public class ActivateUserEmailRequest
    {
        [Required]
        public string ActivateToken { get; set; }

        [Required]
        public int Id { get; set; }

    }
}
