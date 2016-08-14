﻿using UnityEngine;

namespace Assets.Gamelogic
{
    [System.Serializable]
    public struct HexCoordinates
    {
        public readonly int X, Y, Z;

        private HexCoordinates(int x, int z)
        {
            X = x;
            Z = z;
            Y = -x - z;
        }

        public static HexCoordinates FromOffsetCoordinates(int x, int z)
        {
            return new HexCoordinates(x - z / 2, z);
        }

        public override string ToString()
        {
            return "(" +
                X + ", " + Y + ", " + Z + ")";
        }

        public string ToStringOnSeparateLines()
        {
            return X + "\n" + Y + "\n" + Z;
        }

        public static HexCoordinates FromPosition(Vector3 position)
        {
            var x = position.x / (HexMetrics.InnerRadius * 2f);
            var y = -x;

            var offset = position.z / (HexMetrics.OuterRadius * 3f);
            x -= offset;
            y -= offset;

            var iX = Mathf.RoundToInt(x);
            var iY = Mathf.RoundToInt(y);
            var iZ = Mathf.RoundToInt(-x - y);

            if (iX + iY + iZ != 0)
            {
                var dX = Mathf.Abs(x - iX);
                var dY = Mathf.Abs(y - iY);
                var dZ = Mathf.Abs(-x - y - iZ);

                if (dX > dY && dX > dZ)
                {
                    iX = -iY - iZ;
                }
                else if (dZ > dY)
                {
                    iZ = -iX - iY;
                }
            }

            return new HexCoordinates(iX, iZ);
        }
    }
}