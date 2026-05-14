using System;

namespace LiquidSlopesPatch;

partial class ModImpl
{
    public override void Load()
    {
        base.Load();

        throw new Exception("Liquid Slopes Patch has been merged into tModLoader and is no longer required. Please disable it.");
    }
}
