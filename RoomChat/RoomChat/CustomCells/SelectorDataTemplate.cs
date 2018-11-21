using RoomChat.Models;
using Xamarin.Forms;

namespace RoomChat.CustomCells
{
    public class SelectorDataTemplate : DataTemplateSelector
    {
        private readonly DataTemplate textInDataTemplate;
        private readonly DataTemplate textOutDataTemplate;
        private readonly DataTemplate textInImageDataTemplate;
        private readonly DataTemplate textInOptionDataTemplate;

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as Message;

            if (messageVm == null)
                return null;

            if (messageVm.IsTextIn)
                return this.textInDataTemplate;

            if (messageVm.IsImageIn)
                return this.textInImageDataTemplate;

            if (messageVm.IsOptionIn)
                return this.textInOptionDataTemplate;

            return  this.textOutDataTemplate;
        }
        
        public SelectorDataTemplate()
        {
            this.textInDataTemplate = new DataTemplate(typeof(TextInViewCell));
            this.textOutDataTemplate = new DataTemplate(typeof(TextOutViewCell));
            this.textInImageDataTemplate = new DataTemplate(typeof(TextInViewCellimage));
            this.textInOptionDataTemplate = new DataTemplate(typeof(TextInViewCellOption));
        }

    }
}
