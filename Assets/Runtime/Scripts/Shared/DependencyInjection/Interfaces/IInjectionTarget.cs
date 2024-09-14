using System;
using System.Reflection;


public interface IInjectionTarget
{
    //true is safe but slower
    bool waitForAllDependencies { get; }
    Type GetType();

    void FinalizeInjection()
    {
        
    }

    void SetFieldValue(FieldInfo field, object dependency)
    {
        field.SetValue(this, dependency);
    }

    bool FieldIsNull(FieldInfo field)
    {
        return field.GetValue(this) is null;
    }

    object GetRealTarget()
    {
        return this;
    }
}

