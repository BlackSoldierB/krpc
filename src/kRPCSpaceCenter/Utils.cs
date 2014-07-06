using System;
using KSP;
using KRPC.Schema.Geometry;

namespace KRPCSpaceCenter
{
    static class Utils
    {
        /// <summary>
        /// Get the custom action group for a given index (1 through 9)
        /// </summary>
        public static KSPActionGroup GetActionGroup (uint index)
        {
            switch (index) {
            case 1:
                return KSPActionGroup.Custom01;
            case 2:
                return KSPActionGroup.Custom02;
            case 3:
                return KSPActionGroup.Custom03;
            case 4:
                return KSPActionGroup.Custom04;
            case 5:
                return KSPActionGroup.Custom05;
            case 6:
                return KSPActionGroup.Custom06;
            case 7:
                return KSPActionGroup.Custom07;
            case 8:
                return KSPActionGroup.Custom08;
            case 9:
                return KSPActionGroup.Custom09;
            case 0:
                return KSPActionGroup.Custom10;
            default:
                throw new ArgumentException ("Action group index must be between 0 and 9 inclusive");
            }
        }
    }
}

