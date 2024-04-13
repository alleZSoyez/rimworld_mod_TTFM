using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

namespace TatesTinyFurnitureMod
{
    //************* mirror thought 
    [DefOf]
    public static class ThoughtDefOf
    {
        public static ThoughtDef SawSelfInMirror;
    }
    public class MirrorThought : Thought_Situational { }

    //************* the thought maker
    public class ThoughtWorker_Mirror : ThoughtWorker
    {
        public bool MirrorExistsNearby(Pawn p)
        {
            // THIS LINE RIGHT HERE IS THE TROUBLEMAKER
            Thing thisMirror = GenClosest.ClosestThingReachable(p.Position, p.Map, ThingRequest.ForDef(TatesTinyFurnitureMod.ThingDefOf.TatesTinyFurnitureMod_WallMirror), PathEndMode.Touch, TraverseParms.For(p), 5f, null, null, 0, -1, false, RegionType.Set_Passable, true);

            bool returnvalue;

            if (thisMirror != null)
            {
                // make sure the pawn is not facing away from the mirror and that the pawn is before the mirror part
                // for some reason the game uses z value as "y" but it's really z
                // however, we want the square "in front" of the mirror, which is dependent on rotation, otherwise the wall will block it
                Vector2 mirrorRotReadable = thisMirror.Rotation.AsVector2;
                Rot4 mirrorRot = thisMirror.Rotation;
                IntVec3 mirrorPos = thisMirror.Position;
                IntVec3 inFrontOfMirror = new IntVec3();

                // down
                if (mirrorRotReadable == Vector2.down)
                {
                    inFrontOfMirror = mirrorPos - new IntVec3(0, 0, 2);
                }
                // up
                else if (mirrorRotReadable == Vector2.up)
                {
                    inFrontOfMirror = mirrorPos;
                }
                // left/right
                else if (mirrorRotReadable == Vector2.right)
                {
                    inFrontOfMirror = mirrorPos + new IntVec3(1, 0, 0);
                }
                else if (mirrorRotReadable == Vector2.left)
                {
                    inFrontOfMirror = mirrorPos - new IntVec3(1, 0, 0);
                }

                int px = p.Position.x;
                int pz = p.Position.z;

                Vector2 pr = p.Rotation.AsVector2;

                // special thanks to greyfade for helping me here <3
                // which way is the mirror facing, which way is pawn facing, (pawn can't be facing same direction as mirror), and is the pawn in front of the mirror?
                bool readableUp = mirrorRotReadable == Vector2.up && pr != Vector2.up && pz >= inFrontOfMirror.z;
                bool readableDown = mirrorRotReadable == Vector2.down && pr != Vector2.down && pz < inFrontOfMirror.z;
                bool readableLeft = mirrorRotReadable == Vector2.left && pr != Vector2.left && px > inFrontOfMirror.x;
                bool readableRight = mirrorRotReadable == Vector2.right && pr != Vector2.right && px < inFrontOfMirror.x;
                returnvalue = (readableUp || readableDown || readableLeft || readableRight); // this should be true
            }
            else
            {
                returnvalue = false;
            }

            //Log.Message("Mirror exists: " + returnvalue);
            return returnvalue;

        }

        protected override ThoughtState CurrentStateInternal(Pawn p)
        { // is there a mirror nearby?
            if (MirrorExistsNearby(p) == true)
            { // can see?
                if (p.health.capacities.CapableOf(PawnCapacityDefOf.Sight))
                {   // is disfigured? but only in oneself's own opinion (REMOVING THIS FROM LIVE BUILD UNTIL I FIGURE IT OUT)
                    /*if (RelationsUtility.IsDisfigured(p, p))
                    {
                        return ThoughtState.ActiveAtStage(5);
                    }
                    else // not disfigured*/
                    { // has beauty?
                        bool b = p.story.traits.HasTrait(TraitDefOf.Beauty);
                        if (b == true)
                        {   // how much?
                            int d = p.story.traits.DegreeOfTrait(TraitDefOf.Beauty);

                            // get values for final output
                            switch (d)
                            {
                                case -2:
                                    return ThoughtState.ActiveAtStage(1);
                                case -1:
                                    return ThoughtState.ActiveAtStage(2);
                                case 1:
                                    return ThoughtState.ActiveAtStage(3);
                                case 2:
                                    return ThoughtState.ActiveAtStage(4);
                                default:
                                    return ThoughtState.ActiveAtStage(0);
                            }
                        }
                        else { return ThoughtState.ActiveAtStage(0); } // doesn't have beauty trait
                    } // end of disfigurement block
                }
                else { return ThoughtState.Inactive; } // can't see
            }
            else { return ThoughtState.Inactive; } // no mirror
        }
    }
}