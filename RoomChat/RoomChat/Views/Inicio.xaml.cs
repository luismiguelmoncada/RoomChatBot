using Newtonsoft.Json;
using Plugin.Connectivity;
using RoomChat.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoomChat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Inicio : ContentPage
	{             

		public Inicio ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            btniniciarchat.Clicked += btniniciarchat_Clicked;
            btniniciarchatema.Clicked += btniniciarchatema_Clicked;
            btniniciarchatwatson.Clicked += btniniciarchatwatson_Clicked;
        }


        async void btniniciarchat_Clicked(object sender, EventArgs e)
        {
            string result;
            btniniciarchat.IsEnabled = false;
            btniniciarchatwatson.IsEnabled = false;
            btniniciarchatema.IsEnabled = false;
            animationView_Loader.IsVisible = true;

            await Task.Delay(2000);

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(Constantes.BASE_URL);
                    string url = string.Format("/get_clientes.php");
                    var response = await client.GetAsync(url);
                    result = response.Content.ReadAsStringAsync().Result;

                    if (string.IsNullOrEmpty(result) || result == "null")
                    {
                        await DisplayAlert(Constantes.HeaderAlert, "Lo sentimos, no se encontraron datos.", "Aceptar");
                        btniniciarchat.IsEnabled = true;
                        btniniciarchatwatson.IsEnabled = true;
                        btniniciarchatema.IsEnabled = true;
                        animationView_Loader.IsVisible = false;
                        return;
                    }
                    var Jsonrespuesta = JsonConvert.DeserializeObject<ApiResponse>(result);
                    var codigo = string.Format(Jsonrespuesta.codigo);
                    var respuesta = string.Format(Jsonrespuesta.mensaje);

                    if (codigo == Constantes.IS_OK){
                        List<Cliente> clientes = Jsonrespuesta.clientes;
                        //await DisplayAlert("Bien", codigo + " " + respuesta, "Aceptar");    
                        await Navigation.PushAsync(new RoomClientes(clientes));
                        btniniciarchat.IsEnabled = true;
                        btniniciarchatwatson.IsEnabled = true;
                        btniniciarchatema.IsEnabled = true;
                        animationView_Loader.IsVisible = false;
                    }
                    else{
                        await DisplayAlert(Constantes.HeaderAlert, respuesta, "Aceptar");
                        btniniciarchat.IsEnabled = true;
                        btniniciarchatwatson.IsEnabled = true;
                        btniniciarchatema.IsEnabled = true;
                        animationView_Loader.IsVisible = false;
                        return;
                    }                

                }catch(Exception) { 
                    await DisplayAlert(Constantes.HeaderAlert, "Lo sentimos, no hay conexion con el servidor, por favor intenta mas tarde.", "Aceptar");
                    btniniciarchat.IsEnabled = true;
                    btniniciarchatwatson.IsEnabled = true;
                    btniniciarchatema.IsEnabled = true;
                    animationView_Loader.IsVisible = false;
                    return;
                }
            }else{
                await DisplayAlert(Constantes.HeaderAlert, "Lo sentimos, no fue posible establecer conexión a Internet.", "Aceptar");
                btniniciarchat.IsEnabled = true;
                btniniciarchatwatson.IsEnabled = true;
                btniniciarchatema.IsEnabled = true;
                animationView_Loader.IsVisible = false;
                return;
            }            
        }

        async void btniniciarchatema_Clicked(object sender, EventArgs e)
        {
            btniniciarchat.IsEnabled = false;
            btniniciarchatwatson.IsEnabled = false;
            btniniciarchatema.IsEnabled = false;
            animationView_Loader.IsVisible = true;

            await Task.Delay(3000);
            await Navigation.PushAsync(new Chat("7OTwS1dUMilSgJHcF7W66RzbHKmYxK4l_yMTi_1GCC2c", "0462613d-5f5d-484c-b31e-5f2d615830f7", "EMA"));
           
            btniniciarchat.IsEnabled = true;
            btniniciarchatwatson.IsEnabled = true;
            btniniciarchatema.IsEnabled = true;
            animationView_Loader.IsVisible = false;
        }

        async void btniniciarchatwatson_Clicked(object sender, EventArgs e)
        {
            btniniciarchat.IsEnabled = false;
            btniniciarchatwatson.IsEnabled = false;
            btniniciarchatema.IsEnabled = false;
            animationView_Loader.IsVisible = true;

            await Task.Delay(3000);           
            await Navigation.PushAsync(new Chat("7OTwS1dUMilSgJHcF7W66RzbHKmYxK4l_yMTi_1GCC2c", "a1d09aa2-8355-4ad1-9c80-3a9c6a0d34a6", "WATSON"));

            btniniciarchat.IsEnabled = true;
            btniniciarchatwatson.IsEnabled = true;
            btniniciarchatema.IsEnabled = true;
            animationView_Loader.IsVisible = false;
        }
    }
}