using RoomChat.Models;
using RoomChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoomChat.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoomClientes : ContentPage
	{
        RoomClientesViewModel vm;

        public RoomClientes(List<Cliente> clientes)
		{
			InitializeComponent ();
            BindingContext = vm = new RoomClientesViewModel(this,clientes);
        }
	}
}