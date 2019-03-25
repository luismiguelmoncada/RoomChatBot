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
        bool _loaderBot_IsVisible = false;
        public bool LoaderBot_IsVisible
        {
            get => _loaderBot_IsVisible;
            set => SetProperty(ref _loaderBot_IsVisible, value);
        }

        public RoomClientesViewModel(Page page, List<Cliente> clientes)
        {
            this.page = page;
            ListClientes = new ObservableRangeCollection<Cliente>();

            ConnectBootCommand = new Command( async (clientaux) =>
            {
                Cliente cliente = new Cliente();
                cliente = (Cliente)clientaux;
                LoaderBot_IsVisible = true;
                await Task.Delay(3000);
                await page.Navigation.PushAsync(new Chat(cliente.WatsonApi, cliente.WatsonBoot, cliente.Nombre));
                LoaderBot_IsVisible = false;
                return;
            });

            foreach (Cliente cliente in clientes)
            {
                var cli = new Cliente();
                cli.Nombre = cliente.Nombre;
                cli.IdBoot = cliente.IdBoot;
                cli.WatsonApi = cliente.WatsonApi;
                cli.WatsonBoot = cliente.WatsonBoot;
                cli.ConnectBootCommand = ConnectBootCommand;
                cli.clientaux = cli;
                ListClientes.Add(cli);
            }

        }
    }
}
