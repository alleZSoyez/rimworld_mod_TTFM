using Verse;
using UnityEngine;
using System.Collections.Generic;

namespace TatesTinyFurnitureMod
{
    //************ mirrors should only attach to walls
    public class PlaceWorker_TTFM_MirrorsOnlyOnWalls : PlaceWorker
    {
        // the mirror-able walls list
        public static List<ThingDef> listOfWalls = new List<ThingDef>
        {
            // vanilla walls & smoothed stone
            ThingDef.Named("Wall"),
            ThingDef.Named("SmoothedSandstone"),
            ThingDef.Named("SmoothedLimestone"),
            ThingDef.Named("SmoothedMarble"),
            ThingDef.Named("SmoothedGranite"),
            ThingDef.Named("SmoothedSlate"),
        };

        //************* ACTUALLY ADD MORE MOD ITEMS RIGHT HERE

        // GloomyFurniture
        // let's put the rest back once we know exactly which mod is incompatible with GloomyFurniture + TTFM
        //bool GloomyFurniture = TTFM_ModCheckyTime.isModLoaded("GloomyFurniture", new List<string> { "GL_WindowWall", "RGK_WindowWall", "GL_VentWall", "RGK_VentWall", "GL_Wall", "RGK_Wall" });
        bool GloomyFurniture = TTFM_ModCheckyTime.isModLoaded("GloomyFurniture", new List<string> { "GL_Wall", "RGK_Wall" }); 

        //*** draw the part that we need unobstructed
        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol, Thing thing = null)
        {
            Map currentMap = Find.CurrentMap;
            IntVec3 inFrontOfMirror = new IntVec3();

            // this finds the tile "in front" of the mirror
            if (rot.AsVector2 == Vector2.down)
            {
                inFrontOfMirror = center + IntVec3.North.RotatedBy(rot);

            }
            else if (rot.AsVector2 == Vector2.up)
            {
                inFrontOfMirror = center + IntVec3.North.RotatedBy(rot) - new IntVec3(0, 0, 1);
            }
            else
            {
                inFrontOfMirror = center + IntVec3.South.RotatedBy(rot);
            }

            GenDraw.DrawFieldEdges(new List<IntVec3> { inFrontOfMirror }, Color.white);
        }

        // the actual placeworker
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            Vector2 mr = rot.AsVector2;
            IntVec3 downOneTile = new IntVec3(0, 0, -1);
            IntVec3 upOneTile = new IntVec3(0, 0, 1);
            IntVec3 rightOneTile = new IntVec3(1, 0, 0);
            IntVec3 leftOneTile = new IntVec3(-1, 0, 0);

            IntVec3 inFrontOfMirror = new IntVec3();

            // this finds the tile in front of the mirror
            if (mr == Vector2.down)
            {
                inFrontOfMirror = loc + IntVec3.North.RotatedBy(rot);
            }
            else if (mr == Vector2.up)
            {
                inFrontOfMirror = loc + IntVec3.North.RotatedBy(rot) - new IntVec3(0, 0, 1);
            }
            else
            {
                inFrontOfMirror = loc + IntVec3.South.RotatedBy(rot);
            }

            // get list of everything on/under this square on the grid
            var whatIsHere = map.thingGrid.ThingsAt(loc);
            var whatIsBelowHere = map.thingGrid.ThingsAt(loc + downOneTile);

            // check what is here and what is below here, thanks again to greyfade for help with this
            Thing getHere = null;
            Thing getBelow = null;

            if (whatIsHere != null) {
                    foreach (Thing here in whatIsHere) { 
                        if (listOfWalls.Contains(here.def))
                        {
                           getHere = map.thingGrid.ThingAt(loc, here.def);
                    }
                }
            }

            if (whatIsBelowHere != null)
            {
                foreach (Thing here in whatIsBelowHere)
                {
                    if (listOfWalls.Contains(here.def))
                    {
                        getBelow = map.thingGrid.ThingAt(loc + downOneTile, here.def);
                    }
                }
            }

            // finally assign
            Thing onWall = getHere;
            Thing wallRightBelowMirror = getBelow;

            // front of mirror cannot be blocked by another impassible thing
            if (inFrontOfMirror.Impassable(map))
            {
                return new AcceptanceReport("TTFM_CannotObstructFrontOfMirror".Translate());
            }
            else
            {
                // facing north
                if (mr == Vector2.up)
                {
                    // wall exists below where we want mirror
                    if (wallRightBelowMirror == null || wallRightBelowMirror.Position != loc + downOneTile)
                    {
                        return new AcceptanceReport("TTFM_MirrorMustBeAboveWall".Translate());
                    }
                }
                // facing other direction
                else
                {
                    // wall exists right where we're placing it
                    if (onWall == null || onWall.Position != loc)
                    {
                        return new AcceptanceReport("TTFM_MirrorMustBeOnWall".Translate());
                    }
                }
                return true;
            }
        }
    }
}