using Verse;
using UnityEngine;
using System.Collections.Generic;

namespace TatesTinyFurnitureMod
{
    //************ quilts go on beds
    //************ PLEASE NOTE THAT THE QUILTS ARE ONLY MADE TO GO ON VANILLA BEDS, I MAKE NO GUARANTEES FOR MODDED BEDS, NOR CAN I DRAW CUSTOM QUILTS FOR EVERY BED
    
    
    //************ begin single bed quilt
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
                foreach (Thing itemsHere in whatIsHere)
                {
                    if (listOfSingleBeds.Contains(itemsHere.def))
                    {
                        getHere = map.thingGrid.ThingAt(loc, itemsHere.def);
                        //debug
                        //Log.Message("found bed near " + loc.ToString() + ", bed position is " + getHere.PositionHeld);
                    }
                }
            }

            // finally assign
            Thing thisBed = getHere;

            // must be a bed here
            if (thisBed == null) {
                return new AcceptanceReport("TTFM_QuiltMustBeOnBed".Translate());
            }
            else
            { // AND the quilt must be ON THE BED, NOT THE FLOOR
                if (loc != thisBed.PositionHeld) { 
                    return new AcceptanceReport("TTFM_QuiltMustBeOnBed".Translate()); 
                }
                else
                { // AND the quilt and bed must face the same direction
                    if (thisBed.Rotation.AsVector2 != rot.AsVector2) { 
                        return new AcceptanceReport("TTFM_QuiltMustBeSameDirection".Translate()); 
                    }
                    else
                    {  // place it now!
                        return true;
                    }
                }
            }
        }
    }






    //************ begin double bed quilt
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
                foreach (Thing itemsHere in whatIsHere)
                {
                    if (listOfDoubleBeds.Contains(itemsHere.def))
                    {
                        getHere = map.thingGrid.ThingAt(loc, itemsHere.def);
                        //debug
                        //Log.Message("found bed near " + loc.ToString() + ", bed position is " + getHere.PositionHeld);
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
            { // AND the quilt must be ON THE BED, NOT THE FLOOR
                if (loc != thisBed.PositionHeld)
                {
                    return new AcceptanceReport("TTFM_QuiltMustBeOnBed".Translate());
                }
                else
                { // AND the quilt and bed must face the same direction
                    if (thisBed.Rotation.AsVector2 != rot.AsVector2)
                    {
                        return new AcceptanceReport("TTFM_QuiltMustBeSameDirection".Translate());
                    }
                    else
                    {  // place it now!
                        return true;
                    }
                }
            }
        }
    }
}
