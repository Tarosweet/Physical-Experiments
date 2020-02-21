using System;
using UnityEngine;

namespace Extensions
{
    public static class VectorExtension
    {
        private static float directionTolerance = 0.1f;
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            Zero
        }

        public enum Axis
        {
            X,
            Y,
            Z
        }
        
        public static Vector3 DirectionVectorNormalized(Vector3 currentPosition, Vector3 lastPosition)
        {
            return (currentPosition - lastPosition).normalized;
        }

        public static Direction GetVectorDirection(Vector3 directionVector)
        {
            if (directionVector.y > directionTolerance)
                return Direction.Up;

            if (directionVector.y < directionTolerance)
                return Direction.Down;

            if (directionVector.x > directionTolerance)
                return Direction.Right;

            if (directionVector.x < directionTolerance)
                return Direction.Left;

            return Direction.Zero;
        }

        public static bool IsPassedDistanceInDirection(Vector3 currentPosition, Vector3 lastPosition,Axis axis, float distance)
        {
            return Mathf.Abs(GetAxisFromVector(currentPosition, axis) - GetAxisFromVector(lastPosition, axis)) >
                   distance;
        }

        private static float GetAxisFromVector(Vector3 vector, Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    return vector.x;
                case Axis.Y:
                    return vector.y;
                case Axis.Z:
                    return vector.z;
                default:
                    return vector.y;
            }
        }
    }
}
