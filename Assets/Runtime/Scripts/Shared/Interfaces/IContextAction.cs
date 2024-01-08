using UnityEngine;

public interface IContextAction
{
    string actionTitle { get; }
    void DoAction();
}