using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitavox.Models
{
    interface IOnPageKeyPress
    {
        public bool OnPageKeyPress(int keyCode, int keyModifiers);

        public bool OnPageKeyDown(int keyCode);
    }
}
