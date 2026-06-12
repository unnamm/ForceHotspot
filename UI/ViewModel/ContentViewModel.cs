using Common;
using Common.Message;
using Common.Model;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public partial class ContentViewModel
    {
        public Log LogInstance { get; }

        public NetworkData Data { get; }

        public ContentViewModel(Log log, NetworkData networkData)
        {
            LogInstance = log;
            Data = networkData;
        }

        [RelayCommand]
        private void Run()
        {
            WeakReferenceMessenger.Default.Send(new ContentRunMessage());
        }
    }
}
