using RimWorld;
using System.Collections.Generic;
using Verse;

namespace TatesTinyFurnitureMod
{
    //************ define the mirror as a def so mirrorthought works.
    [DefOf]
    public static class ThingDefOf
    {
        public static ThingDef TatesTinyFurnitureMod_WallMirror
        {
            get
            {
                return (ThingDef.Named("TatesTinyFurnitureMod_WallMirror"));
            }
        }

    }
    public class Building_Mirror_TTFM : Building { }

}