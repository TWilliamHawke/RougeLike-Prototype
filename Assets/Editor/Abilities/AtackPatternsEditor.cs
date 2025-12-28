using System.Collections.Generic;
using Abilities;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AttackPatterns))]
public partial class AtackPatternsEditor : AOEPatternsEditor
{
    AttackPatterns pattern => target as AttackPatterns;

    protected override void DrawIcons(AttackPatternData patternData, Dictionary<Vector3Int, int> targets, Vector3Int position, Rect rect)
    {
        if (position.x != 0 || position.y != 0)
        {
            DrawTargets(patternData, targets, position, rect);
        }
        else
        {
            GUI.DrawTexture(rect, _centerIcon);
        }
    }

    protected override void DrawTargetIcon(Rect rect, Vector3Int position, AttackPatternData tilingRule)
    {
        Texture2D icon = tilingRule.hitPosition == position ? _hitIcon : _targetIcon;
        GUI.DrawTexture(rect, icon);
    }

    protected override void UpdatePattern(AttackPatternData pattern, Vector3Int position)
    {
        pattern.UpdateTargetsAndHit(position);
    }

    protected override void SaveTile()
    {
        base.SaveTile();
        pattern.CreatePatterns();
    }


}
