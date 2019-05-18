using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanMaoKhe.ModuleOPC
{
    public enum eQualiti
    {
        OPCQualityBad = 0,
        OPCQualityUncertain = 64,
        OPCQualityGood = 192
    }

    public enum eKepserverStatus
    {
        OFF = 0,
        ON = 1,
        PAUSE = 2,
    }
}
