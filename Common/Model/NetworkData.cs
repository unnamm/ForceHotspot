using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    /// <summary>
    /// content view binding
    /// </summary>
    public partial class NetworkData : ObservableObject
    {
        [ObservableProperty] string _id = string.Empty;
        [ObservableProperty] string _password = string.Empty;
    }
}
