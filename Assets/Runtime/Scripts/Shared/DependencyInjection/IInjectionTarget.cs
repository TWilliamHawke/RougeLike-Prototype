using System;
using System.Reflection;


public interface IInjectionTarget
{
    //true is safe but slower
    bool waitForAllDependencies { get; }

    void FinalizeInjection();
    Type GetType();

    void SetValue(FieldInfo field, object dependency)
    {
        field.SetValue(this, dependency);
    }

    bool FieldIsNull(FieldInfo field)
    {
        return field.GetValue(this) is null;
    }
}

