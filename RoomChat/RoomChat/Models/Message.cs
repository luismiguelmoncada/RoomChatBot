using System;
using System.Collections.Generic;
using System.Windows.Input;
using MvvmHelpers;

namespace RoomChat.Models
{
    public class Message : ObservableObject
    {
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
        string _text;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        string _title;

        public string Source
        {
            get { return _source; }
            set { SetProperty(ref _source, value); }
        }
        string _source;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        string _description;

        public DateTime MessageDateTime
        {
            get { return _messageDateTime; }
            set { SetProperty(ref _messageDateTime, value); }
        }

        DateTime _messageDateTime;

        public string TimeDisplay => MessageDateTime.ToLocalTime().ToString();


        public List<Option> Options { get; set; }

        public ICommand SendCommandOption { get; set; }


        public bool IsTextIn
        {
            get { return _isTextIn; }
            set { SetProperty(ref _isTextIn, value); }
        }

        public bool IsImageIn
        {
            get { return _IsImageIn; }
            set { SetProperty(ref _IsImageIn, value); }
        }

        public bool IsOptionIn
        {
            get { return _IsOptionIn; }
            set { SetProperty(ref _IsOptionIn, value); }
        }


        bool _isTextIn;
        bool _IsImageIn;
        bool _IsOptionIn;
    }
}

