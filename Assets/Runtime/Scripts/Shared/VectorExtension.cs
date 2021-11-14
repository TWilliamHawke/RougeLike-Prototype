using UnityEngine;

public static class VectorExtension
{
    public static Vector3Int ToInt(this Vector3 vc)
    {
        int x = ToInt(vc.x);
        int y = ToInt(vc.y);
        int z = ToInt(vc.z);

        return new Vector3Int(x, y, z);
    }

    public static Vector2Int Toint(this Vector2 vc)
    {
        int x = ToInt(vc.x);
        int y = ToInt(vc.y);

        return new Vector2Int(x,y);
    }

    public static Vector3Int AddZ(this Vector2Int vc, int z)
    {
        return new Vector3Int(vc.x, vc.y, z);
    }

    public static Vector3 AddZ(this Vector2 vc, float z)
    {
        return new Vector3(vc.x, vc.y, z);
    }

    public static Vector3Int ToTilePos(this Vector3 vc)
    {
        int x = ToInt(vc.x);
        int y = ToInt(vc.y);

        return new Vector3Int(x, y, 0);
    }

    public static Vector3 ChangeXYFrom(this Vector3 current, Vector3 other)
    {
        float z = current.z;
        return new Vector3(other.x, other.y, z);
    }


    static int ToInt(float num)
    {
        return Mathf.FloorToInt(num);
    }

}
