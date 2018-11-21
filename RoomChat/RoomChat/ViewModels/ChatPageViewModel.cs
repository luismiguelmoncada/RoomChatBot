using IBM.WatsonDeveloperCloud.Assistant.v1;
using IBM.WatsonDeveloperCloud.Assistant.v1.Model;
using IBM.WatsonDeveloperCloud.Util;
using MvvmHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using RoomChat.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;


namespace RoomChat.ViewModels
{
    class ChatPageViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Message> ListMessages { get; }
        public ICommand SendCommand { get; set; }
        public ICommand SendCommandOption { get; set; }
        private AssistantService _assistant;
        private Page page;
        private JToken context;
        private string ApiKey;
        private string Boot;
        private List<ResponseGeneric> listaitemsRespuesta;

        public ChatPageViewModel(Page page, string apiKey, string boot)
        {
            this.page = page;
            Boot = boot;
            ApiKey = apiKey;
            ListMessages = new ObservableRangeCollection<Message>();  

            //Inicia el servicio bot por primer vez vez
            TokenOptions iamAssistantTokenOptions = new TokenOptions()
            {
                IamApiKey = ApiKey,
                ServiceUrl = "https://gateway.watsonplatform.net/assistant/api"
            };
            _assistant = new AssistantService(iamAssistantTokenOptions, "2018-09-20");           
            //ConversationService _conversation = new ConversationService("dd01579d-833b-4c11-8a23-471ee5570551", "j4OHSlTqkR8v", "2018-11-05");

            //Siempre se arma un primer mensaje con un string vacio para que el boot salude
            MessageRequest messageRequest0 = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = ""
                },
            };

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    var result0 = _assistant.Message(Boot, messageRequest0);
                    var respuesta0 = JObject.Parse(result0.ResponseJson);
                    var respuestamensaje0 = respuesta0["output"]["text"];
                    context = respuesta0["context"];
                    listaitemsRespuesta = JsonConvert.DeserializeObject<List<ResponseGeneric>>(respuesta0["output"]["generic"].ToString());
                    showMessageResponseBot(listaitemsRespuesta);
                }
                catch (Exception)
                {
                    page.DisplayAlert(Constantes.HeaderAlert, "Lo sentimos, no hay conexion con el servidor, por favor intenta mas tarde.", "Aceptar");
                    return;
                }
            }else {
                page.DisplayAlert(Constantes.HeaderAlert, "Lo sentimos, no fue posible establecer conexión a Internet.", "Aceptar");               
                return;
            }          

            SendCommand = new Command(() =>
            {
                if (!String.IsNullOrWhiteSpace(OutText))
                {
                    var message = new Message
                    {
                        Text = OutText,
                        IsTextIn = false,
                        MessageDateTime = DateTime.Now
                    };

                    ListMessages.Add(message);

                    MessageRequest messageRequest = new MessageRequest()
                    {
                        Input = new InputData()
                        {
                            Text = OutText
                        },
                        Context = context
                    };

                    OutText = "";
                    sendMessageBot(messageRequest);
                }
                else {
                    page.DisplayAlert("RoomChat", "Lo siento, debes ingresar un texto para continuar.", "OK");
                }
            });


            SendCommandOption = new Command((parametro) =>
            {
                var messageOptions = new Message
                {
                    Text = parametro.ToString(),
                    IsTextIn = false,
                    MessageDateTime = DateTime.Now
                };

                ListMessages.Add(messageOptions);

                MessageRequest messageRequestOptions = new MessageRequest()
                {
                    Input = new InputData()
                    {
                        Text = parametro.ToString()
                    },
                    Context = context
                };                               
                sendMessageBot(messageRequestOptions);
            });
        }
        
        private async void sendMessageBot(MessageRequest messagerequest) {           
            await Task.Delay(800);
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    var result0 = _assistant.Message(Boot, messagerequest);
                    var respuesta0 = JObject.Parse(result0.ResponseJson);
                    var respuestamensaje0 = respuesta0["output"]["text"];
                    context = respuesta0["context"];
                    listaitemsRespuesta = JsonConvert.DeserializeObject<List<ResponseGeneric>>(respuesta0["output"]["generic"].ToString());
                    showMessageResponseBot(listaitemsRespuesta);
                }
                catch (Exception)
                {
                    await page.DisplayAlert(Constantes.HeaderAlert, "Lo sentimos, no hay conexion con el servidor, por favor intenta mas tarde.", "Aceptar");
                    return;
                }
            }
            else
            {
                await page.DisplayAlert(Constantes.HeaderAlert, "Lo sentimos, no fue posible establecer conexión a Internet.", "Aceptar");
                return;
            }            
            //Console.WriteLine(respuesta0["output"]["generic"].ToString());
            //await page.DisplayAlert("RoomChat", respuesta0["output"]["generic"].ToString(), "OK");                       
        }

        private void showMessageResponseBot(List<ResponseGeneric> listaitemsRespuesta)
        {
            foreach (ResponseGeneric itemrespuesta in listaitemsRespuesta)
            {
                if (itemrespuesta.response_type == "text")
                {
                    var messageTypeText = new Message
                    {
                        Text = itemrespuesta.text,
                        IsTextIn = true,
                        MessageDateTime = DateTime.Now,
                    };
                    ListMessages.Add(messageTypeText);
                }
                if (itemrespuesta.response_type == "image")
                {
                    var messageTypeImage = new Message
                    {
                        Text = itemrespuesta.title,
                        Description = itemrespuesta.description,
                        IsImageIn = true,
                        MessageDateTime = DateTime.Now,
                        Source = itemrespuesta.source,

                    };
                    ListMessages.Add(messageTypeImage);
                }
                if (itemrespuesta.response_type == "option")
                {
                    List<Option> availableOptions = new List<Option>();
                    foreach (Option items in itemrespuesta.options)
                    {
                        var optionsClic = new Option();
                        optionsClic.label = items.label;
                        optionsClic.finalvalue = items.value.input.text;
                        optionsClic.SendCommandOption = SendCommandOption;
                        availableOptions.Add(optionsClic);
                    }
                    //options = itemrespuesta.options;
                    var messageTypeOption = new Message
                    {
                        Text = itemrespuesta.title,
                        Description = itemrespuesta.description,
                        Options = availableOptions,
                        IsOptionIn = true,
                        MessageDateTime = DateTime.Now,
                        SendCommandOption = SendCommandOption,
                    };
                    ListMessages.Add(messageTypeOption);
                }
            }

        }

        public string OutText
        {
            get { return _outText; }
            set { SetProperty(ref _outText, value); }
        }
        string _outText = string.Empty;
    }
}
