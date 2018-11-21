using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoomChat.CustomCells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TextInViewCellOption : ViewCell
	{

        public ICommand SendCommandOption { get; set; }


        public TextInViewCellOption ()
		{
			InitializeComponent ();

            

         

            //Label labeltittle = new Label
            //{                
            //    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            //    VerticalOptions = LayoutOptions.CenterAndExpand,
            //    HorizontalOptions = LayoutOptions.Center
            //};

            //Label labeldescription = new Label
            //{
            //    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            //    VerticalOptions = LayoutOptions.CenterAndExpand,
            //    HorizontalOptions = LayoutOptions.Center
            //};

            //Button button = new Button
            //{
            //    Text = "Click to Rotate Text!",
            //    VerticalOptions = LayoutOptions.CenterAndExpand,
            //    HorizontalOptions = LayoutOptions.Center
            //};

            //labeltittle.SetBinding(Label.TextProperty, new Binding("Text", BindingMode.TwoWay));
            //labeldescription.SetBinding(Label.TextProperty, new Binding("Description", BindingMode.TwoWay));

            //View = new StackLayout
            //{
            //    Children =
            //{
            //    labeltittle,
            //    labeldescription,
            //    button
            //}
            //};


        }
    }
}