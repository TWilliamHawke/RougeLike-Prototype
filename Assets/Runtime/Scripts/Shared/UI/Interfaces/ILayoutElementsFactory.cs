using System.Collections.Generic;

public interface ILayoutElementsFactory<T>
{
    IEnumerable<T> CreateElements(IUILayout<T> parent);
}
