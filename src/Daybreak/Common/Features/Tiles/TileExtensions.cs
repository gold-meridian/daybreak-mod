using Terraria.ID;

namespace Daybreak.Common.Features.Tiles;

public static class TileExtensions
{
    extension(SlopeType slope)
    {
        public bool Block => slope == SlopeType.Solid;

        public bool Up => slope.Block || slope is SlopeType.SlopeUpLeft or SlopeType.SlopeUpRight;

        public bool Down => slope.Block || slope is SlopeType.SlopeDownLeft or SlopeType.SlopeDownRight;

        public bool Left => slope.Block || slope is SlopeType.SlopeUpLeft or SlopeType.SlopeDownLeft;

        public bool Right => slope.Block || slope is SlopeType.SlopeUpRight or SlopeType.SlopeDownRight;

        public bool UpLeft => slope.Up && slope.Left;

        public bool DownLeft => slope.Down && slope.Left;

        public bool UpRight => slope.Up && slope.Right;

        public bool DownRight => slope.Down && slope.Right;
    }
}
