using UnityEngine;

public interface IContextAction
{
    string actionTitle { get; }
    void DoAction();
    bool closeBackgroundScreen { get; }
}