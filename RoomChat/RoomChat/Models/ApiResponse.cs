using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RoomChat.Models
{
    class ApiResponse
    {
        public string codigo { get; set; }
        public string mensaje { get; set; }
        public List<Cliente> clientes { get; set; }
    }

    public class Cliente
    {
        public string IdCliente { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Icono { get; set; }
        public string FechaCreacion { get; set; }
        public string Estado { get; set; }
        public string IdBoot { get; set; }
        public string Descripcion { get; set; }
        public string WatsonApi { get; set; }
        public string WatsonBoot { get; set; }
        public ICommand ConnectBootCommand { get; set; }
    }

    
}
