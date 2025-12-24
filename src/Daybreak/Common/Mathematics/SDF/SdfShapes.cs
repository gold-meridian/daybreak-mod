using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Daybreak.Common.Mathematics;

/* Credit to Verminoid Creature for the original implementation, based on:
 * <https://github.com/JasperDawg/Cataphract/blob/10fe993da52d558d3bba2fe49237195701a2b6a4/Common/Worldgen.cs>.
 *
 * Generously licensed to us under AGPL v3.0.
 */

/// <summary>
///     Provides helpers to generating common SDF shapes.
/// </summary>
public static class SdfShapes
{
    // https://iquilezles.org/articles/distgradfunctions2d/

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample Circle(
        Vector2 p,
        float radius,
        Angle? rotation = null
    )
    {
        rotation ??= Angle.Zero;
        {
            p = p.RotatedBy(rotation.Value);
        }

        var len = p.Length();
        var dist = len - radius;
        var grad = len > float.Epsilon ? p / len : Vector2.UnitY;
        return new SdfSample(dist, grad);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample Box(
        Vector2 p,
        Vector2 halfExtents,
        Angle? rotation = null
    )
    {
        rotation ??= Angle.Zero;
        {
            p = p.RotatedBy(rotation.Value);
        }
        
        
        var w = new Vector2(MathF.Abs(p.X), MathF.Abs(p.Y));
        var q = w - halfExtents;
        var max = Vector2.Max(q, Vector2.Zero);
        var outside = max.Length();
        var inside = MathF.Min(MathF.Max(q.X, q.Y), 0f);
        var dist = outside + inside;

        Vector2 grad;
        if (outside > float.Epsilon)
        {
            var n = max / outside;
            grad = new Vector2(MathF.Sign(p.X) * n.X, MathF.Sign(p.Y) * n.Y);
        }
        else
        {
            if (q.X > q.Y)
            {
                grad = new Vector2(MathF.Sign(p.X), 0f);
            }
            else if (q.Y > q.X)
            {
                grad = new Vector2(0f, MathF.Sign(p.Y));
            }
            else
            {
                grad = Vector2.UnitY;
            }
        }

        return new SdfSample(dist, grad);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample RoundedBox(
        Vector2 p,
        Vector2 halfExtents,
        float round,
        Angle? rotation = null
    )
    {
        rotation ??= Angle.Zero;
        {
            p = p.RotatedBy(rotation.Value);
        }
        
        Vector2 w = new(MathF.Abs(p.X), MathF.Abs(p.Y));
        var q = w - halfExtents + new Vector2(round);
        var max = Vector2.Max(q, Vector2.Zero);
        var outside = max.Length() - round;
        var inside = MathF.Min(MathF.Max(q.X, q.Y), 0f);
        var dist = outside + inside;

        Vector2 grad;
        if (max.Length() > float.Epsilon)
        {
            var n = max / max.Length();
            grad = new Vector2(MathF.Sign(p.X) * n.X, MathF.Sign(p.Y) * n.Y);
        }
        else
        {
            if (q.X > q.Y)
            {
                grad = new Vector2(MathF.Sign(p.X), 0f);
            }
            else if (q.Y > q.X)
            {
                grad = new Vector2(0f, MathF.Sign(p.Y));
            }
            else
            {
                grad = Vector2.UnitY;
            }
        }

        return new SdfSample(dist, grad);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample Segment(
        Vector2 p,
        Vector2 a,
        Vector2 b,
        Angle? rotation = null
    )
    {
        rotation ??= Angle.Zero;
        {
            p = p.RotatedBy(rotation.Value);
            a = a.RotatedBy(rotation.Value);
            b = b.RotatedBy(rotation.Value);
        }
        
        
        var pa = p - a;
        var ba = b - a;
        var denom = Vector2.Dot(ba, ba);
        var h = denom > float.Epsilon ? MathHelper.Clamp(Vector2.Dot(pa, ba) / denom, 0f, 1f) : 0f;
        var closest = a + ba * h;
        var diff = p - closest;
        var len = diff.Length();
        var grad = len > float.Epsilon ? diff / len : Vector2.UnitY;
        return new SdfSample(len, grad);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample Annulus(
        Vector2 p,
        float innerRadius,
        float outerRadius,
        Angle? rotation = null
    )
    {
        rotation ??= Angle.Zero;
        {
            p = p.RotatedBy(rotation.Value);
        }

        var len = p.Length();
        var dist = MathF.Max(len - outerRadius, innerRadius - len);

        Vector2 grad;
        if (len <= float.Epsilon)
        {
            grad = Vector2.UnitY;
        }
        else if (len > outerRadius)
        {
            grad = p / len;
        }
        else if (len < innerRadius)
        {
            grad = -p / len;
        }
        else
        {
            grad = Vector2.UnitY;
        }

        return new SdfSample(dist, grad);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample Polygon(
        Vector2 p,
        ReadOnlySpan<Vector2> vertices,
        Angle? rotation = null
    )
    {
        if (vertices.IsEmpty)
        {
            return new SdfSample(float.PositiveInfinity, Vector2.UnitY);
        }

        rotation ??= Angle.Zero;
        {
            p = p.RotatedBy(rotation.Value);
        }

        var minDist = float.MaxValue;
        var closestVec = Vector2.UnitY;
        var inside = false;

        for (var i = 0; i < vertices.Length; i++)
        {
            var vi = vertices[i].RotatedBy(rotation.Value);
            var vj = vertices[(i + 1) % vertices.Length].RotatedBy(rotation.Value);

            var seg = Segment(p, vi, vj);
            if (seg.Distance < minDist)
            {
                minDist = seg.Distance;
                closestVec = seg.Gradient;
            }

            if (
                (vi.Y > p.Y != vj.Y > p.Y)
             && (p.X < (vj.X - vi.X) * (p.Y - vi.Y) / (vj.Y - vi.Y + float.Epsilon) + vi.X)
            )
            {
                inside = !inside;
            }
        }

        var dist = inside ? -minDist : minDist;
        return new SdfSample(dist, closestVec);
    }

    /*
    #region Circle
        // https://www.shadertoy.com/view/WltSDj
    #endregion

    #region Pie
        // https://www.shadertoy.com/view/3tGXRc
    #endregion

    #region Arc
        // https://www.shadertoy.com/view/WtGXRc
    #endregion

    #region Segment
        // https://www.shadertoy.com/view/WtdSDj
    #endregion

    #region Vesica
        // https://www.shadertoy.com/view/3lGXRc
    #endregion

    #region Box
        // https://www.shadertoy.com/view/wlcXD2
    #endregion

    #region Cross
        // https://www.shadertoy.com/view/WtdXWj
    #endregion

    #region Pentagon
        // https://www.shadertoy.com/view/3lySRc
    #endregion

    #region Hexagon
        // https://www.shadertoy.com/view/WtySRc
    #endregion

    #region Isosceles Triangle
        // https://www.shadertoy.com/view/3dyfDd
    #endregion

    #region Triangle
        // https://www.shadertoy.com/view/tlVyWh
    #endregion

    #region Quad
        // https://www.shadertoy.com/view/WtVcD1
    #endregion

    #region Ellipse
        // https://www.shadertoy.com/view/3lcfR8
    #endregion

    #region Moon
        // https://www.shadertoy.com/view/ddX3WH
    #endregion

    #region Parabola
        // https://www.shadertoy.com/view/mdX3WH
    #endregion

    #region Trapezoid
        // https://www.shadertoy.com/view/ddt3Rs
    #endregion

    #region Heart
        // https://www.shadertoy.com/view/DldXRf
    #endregion

    #region Rounded Box
    #endregion

    #region Annulus
    #endregion

    #region Polygon
    #endregion
    */
}
