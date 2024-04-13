using RimWorld;
using Verse;
using UnityEngine;

namespace TatesTinyFurnitureMod

{//************ minified classes

    // minified picnic blanket
    public class MinifiedPicnicBlanket : MinifiedThing
    {
        private Graphic cachedGraphic;
        private const float MaxMinifiedGraphicSize = 0.5f;
        private const float CrateToGraphicScale = 1.16f;

        private Vector2 GetMinifiedDrawSize(Vector2 drawSize, float maxSideLength)
        {
            float num = maxSideLength / Mathf.Max(drawSize.x, drawSize.y);
            if (num >= 1f)
            {
                return drawSize;
            }
            return drawSize * num;
        }

        public override Graphic Graphic
        {
            get
            {
                if (this.cachedGraphic == null)
                {
                    this.cachedGraphic = this.InnerThing.Graphic.ExtractInnerGraphicFor(this.InnerThing);
                    if ((float)this.InnerThing.def.size.x > 0.5f || (float)this.InnerThing.def.size.z > 0.5f)
                    {
                        Vector2 minifiedDrawSize = this.GetMinifiedDrawSize(this.InnerThing.def.size.ToVector2(), 0.5f);
                        Vector2 newDrawSize = new Vector2(minifiedDrawSize.x / (float)this.InnerThing.def.size.x * this.cachedGraphic.drawSize.x, minifiedDrawSize.y / (float)this.InnerThing.def.size.z * this.cachedGraphic.drawSize.y);
                        this.cachedGraphic = this.cachedGraphic.GetCopy(newDrawSize);
                    }
                }
                return this.cachedGraphic;
            }
        }
    }

    // minified mirror
    public class MinifiedMirror : MinifiedThing
    {
        private Graphic crateFrontGraphic;
        private const float MaxMinifiedGraphicSize = 1.1f;
        private const float CrateToGraphicScale = 1.16f;

        private Vector2 GetMinifiedDrawSize(Vector2 drawSize, float maxSideLength)
        {
            float num = maxSideLength / Mathf.Max(drawSize.x, drawSize.y);
            if (num >= 1f)
            {
                return drawSize;
            }
            return drawSize * num;
        }

        // no idea why it insists on being protected now but w/e
        protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            if (this.crateFrontGraphic == null)
            {
                this.crateFrontGraphic = GraphicDatabase.Get<Graphic_Single>("Things/Item/Minified/CrateFront", ShaderDatabase.Cutout, this.GetMinifiedDrawSize(this.InnerThing.def.size.ToVector2(), 1.1f) * 1.16f, Color.white);
            }
            this.crateFrontGraphic.DrawFromDef(drawLoc + Altitudes.AltIncVect * 0.1f, Rot4.North, null, 0f);
            this.Graphic.Draw(drawLoc + (new Vector3(0, 0, 0.25f)), Rot4.South, this, 0f);

        }
    }
}
