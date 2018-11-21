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
	public partial class Chat : ContentPage
	{
        ChatPageViewModel vm;

        public Chat (string apiKey,string boot, string bootnombre)
		{
			InitializeComponent ();
            Title = string.Format("{0}", bootnombre);
            BindingContext = vm = new ChatPageViewModel(this, apiKey, boot);

            vm.ListMessages.CollectionChanged += (sender, e) =>
            {
                //var target = vm.ListMessages[vm.ListMessages.Count - 1];
                //MessagesListView.ScrollTo(target, ScrollToPosition.End, true);
                //MessagesListView.ScrollTo((MessagesListView.ItemsSource as IEnumerable<object>).Last(), ScrollToPosition.End, true);

                var lastItem = MessagesListView.ItemsSource.OfType<object>().Last();
                MessagesListView.ScrollTo(lastItem, ScrollToPosition.MakeVisible, true);
            };
            
        }
	}
}