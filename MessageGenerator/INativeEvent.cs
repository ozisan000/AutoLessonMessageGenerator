using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGenerator
{
    public interface INativeEvent
    {
        public void InvokeEvent(IntPtr hWnd, IntPtr wParam, IntPtr lParam);
    }
}
