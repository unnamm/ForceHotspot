using Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Config
{
    [SettingMapper("setting")]
    public class SettingData : YamlBase
    {
        [SettingMember("IanaInterfaceType", ConvertType.Text)]
        public uint IanaInterfaceType { get; set; }
    }
}
