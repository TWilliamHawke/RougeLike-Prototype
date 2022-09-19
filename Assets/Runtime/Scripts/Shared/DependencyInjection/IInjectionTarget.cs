public interface IInjectionTarget
{
    void FinalizeInjection();
    //true is safe but slower
    bool waitForAllDependencies { get; }
}

