using Sistema_Escolar.Models.Enums;

namespace Sistema_Escolar.Models.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public TipoUsuario Tipo { get; set; }
    }
}
