using Verse;
using UnityEngine;
using System.Collections.Generic;

namespace TatesTinyFurnitureMod
{
    //************ quilts go on beds
    //************ PLEASE NOTE THAT THE QUILTS ARE ONLY MADE TO GO ON VANILLA BEDS, I MAKE NO GUARANTEES FOR MODDED BEDS, NOR CAN I DRAW CUSTOM QUILTS FOR EVERY BED
    public class PlaceWorker_TTFM_BedQuiltSingle : PlaceWorker
    {

        // bed list (single)
        public static List<ThingDef> listOfSingleBeds = new List<ThingDef>
        {
            // single beds list
            ThingDef.Named("Bed"),
            ThingDef.Named("HospitalBed"),
        };

        // the actual placeworker
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            var whatIsHere = map.thingGrid.ThingsAt(loc);
            //Log.Message("started");

            // check what is here, thanks again to greyfade for help with this
            Thing getHere = null;

            if (whatIsHere != null)
            {
                foreach (Thing here in whatIsHere)
                {
                    if (listOfSingleBeds.Contains(here.def))
                    {
                        getHere = map.thingGrid.ThingAt(loc, here.def);
                        //Log.Message("found bed");
                    }
                }
            }

            // finally assign
            Thing thisBed = getHere;

            // must be a bed here
            if (thisBed == null)
            {
                return new AcceptanceReport("TTFM_QuiltMustBeOnBed".Translate());
            }
            else
            {
                // quilt and bed must face the same direction
                if (thisBed.Rotation.AsVector2 != rot.AsVector2)
                {
                    return new AcceptanceReport("TTFM_QuiltMustBeSameDirection".Translate());
                }
                else
                {
                    return true;
                }
            }
        }
    }

    public class PlaceWorker_TTFM_BedQuiltDouble : PlaceWorker
    {

        // bed list (double)
        public static List<ThingDef> listOfDoubleBeds = new List<ThingDef>
        {
            // double beds list
            ThingDef.Named("DoubleBed"),
            ThingDef.Named("RoyalBed"),
        };

        // the actual placeworker
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            var whatIsHere = map.thingGrid.ThingsAt(loc);
            //Log.Message("started");

            // check what is here, thanks again to greyfade for help with this
            Thing getHere = null;

            if (whatIsHere != null)
            {
                foreach (Thing here in whatIsHere)
                {
                    if (listOfDoubleBeds.Contains(here.def))
                    {
                        getHere = map.thingGrid.ThingAt(loc, here.def);
                        //Log.Message("found bed");
                    }
                }
            }

            // finally assign
            Thing thisBed = getHere;

            // must be a bed here
            if (thisBed == null)
            {
                return new AcceptanceReport("TTFM_QuiltMustBeOnBed".Translate());
            }
            else
            {
                // quilt and bed must face the same direction
                if (thisBed.Rotation.AsVector2 != rot.AsVector2)
                {
                    return new AcceptanceReport("TTFM_QuiltMustBeSameDirection".Translate());
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
