using UnityEngine;

public struct Matrix3x3Lite
    {
        public Vector3 xAxis { get; init; }
        public Vector3 yAxis { get; init; }
        public Vector3 zAxis { get; init; }

        public Matrix3x3Lite(Vector3 xAxis, Vector3 yAxis, Vector3 zAxis = default)
        {
            this.xAxis = xAxis;
            this.yAxis = yAxis;
            this.zAxis = zAxis;
        }

        public static Vector3 operator *(Vector3 vector, Matrix3x3Lite matrix)
        {
            return vector.x * matrix.xAxis + vector.y * matrix.yAxis + vector.z * matrix.zAxis;
        }

        

    }
