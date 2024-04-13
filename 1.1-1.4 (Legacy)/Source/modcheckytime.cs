using System;
using System.Collections.Generic;
using Verse;

namespace TatesTinyFurnitureMod
{
    //************ mod checker function
    public class TTFM_ModCheckyTime
    {
        public static bool isModLoaded(string modName, List<string> itemsToAdd)
        {
            bool result = false;
            if (ModLister.HasActiveModWithName(modName))
            {
                /*
                // debug
                string msg = String.Format("Found mod '{0}', adding objects.", modName);
                Log.Message(msg);
                */

                // add each to the list if it is not there already
                foreach (String item in itemsToAdd)
                {
                    if (!PlaceWorker_TTFM_MirrorsOnlyOnWalls.listOfWalls.Contains(ThingDef.Named(item)))
                    {
                        PlaceWorker_TTFM_MirrorsOnlyOnWalls.listOfWalls.Add(ThingDef.Named(item));
                    }
                }
                result = true;
            }
            else
            {
                /*
                // debug
                string msg = String.Format("Couldn't find mod '{0}', skipped.", modName);
                Log.Message(msg);
                */
                result = false;
            }

            return result;

        }
    }

}
