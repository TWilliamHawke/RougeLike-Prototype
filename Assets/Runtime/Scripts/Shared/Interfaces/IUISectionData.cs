using System.Collections.Generic;
using UnityEngine.Events;

public interface IUISectionData<T> : IUISectionData, IEnumerable<T>
{
}

public interface IUISectionData
{
    int filledSlotsCount { get; }
    int capacity { get; }
    string sectionName { get; }
    event UnityAction OnSectionDataChange;
}
