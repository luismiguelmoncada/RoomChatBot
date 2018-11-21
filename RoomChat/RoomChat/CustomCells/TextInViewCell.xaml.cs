using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoomChat.CustomCells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TextInViewCell : ViewCell
	{
		public TextInViewCell ()
		{
			InitializeComponent ();

            //Label label = new Label
            //{
            //    Text = "Click the Button below",
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
            
            //label.SetBinding(Label.TextProperty, new Binding("Text",BindingMode.TwoWay));

            //View = new StackLayout
            //{
            //    Children =
            //{
            //    label,
            //    button
            //}
            //};


        }
	}
}