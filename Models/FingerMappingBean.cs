using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitavox.Models
{
    public readonly record struct FingerMappingBean(string speak, string show, string code, string speakOnlyChar, string showOnlyChar, string key);
}
