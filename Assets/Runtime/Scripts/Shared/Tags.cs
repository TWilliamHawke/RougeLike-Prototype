using UnityEngine;

[CreateAssetMenu(fileName = "Tag", menuName = "Tags/Tag", order = 1)]
public class Tag : ScriptableObject
{
    
}

[CreateAssetMenu(fileName = "TagList", menuName = "Tags/TagList", order = 2)]
public class TagList : DatabaseSet<Tag>
{
    
}