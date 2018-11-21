using IBM.WatsonDeveloperCloud.Util;
using MvvmHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoomChat.Models;
using RoomChat.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace RoomChat.ViewModels
{
    class RoomClientesViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Cliente> ListClientes { get; }
        public ICommand ConnectBootCommand { get; set; }
        private Page page;

        public RoomClientesViewModel(Page page, List<Cliente> clientes)
        {
            this.page = page;
            ListClientes = new ObservableRangeCollection<Cliente>();
            
            ConnectBootCommand = new Command(async (IdBoot) =>
            {
                foreach (Cliente c in clientes)
                {
                    if (c.IdBoot == IdBoot.ToString()) {
                        if (c.WatsonApi.Length > 0 && c.WatsonBoot.Length > 0) {
                            var action = await page.DisplayAlert(Constantes.HeaderAlert, string.Format("Hola soy {0}, estoy aqui para ayudarte. Bienvenid@", c.Nombre), "INICIAR","CANCELAR");
                            if (action)
                            {
                                string ApiKey = c.WatsonApi;
                                string Boot = c.WatsonBoot;
                                await page.Navigation.PushAsync(new Chat(ApiKey, Boot, c.Nombre));
                            }                          
                        }                       
                    }                   
                }                
            });
            
            foreach (Cliente cliente in clientes)
            {
                var cli = new Cliente
                {
                    Nombre = cliente.Nombre,
                    IdBoot = cliente.IdBoot,
                    ConnectBootCommand = ConnectBootCommand,
                };
                ListClientes.Add(cli);
            }
        }
    }
}
