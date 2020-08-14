using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsJX3AssistantNativeHelper.Kits
{
    public interface IHttpPortChangedObserver
    {
        void HttpPortChanged(int port);
    }
}
