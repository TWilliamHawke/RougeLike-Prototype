interface IDependencyProvider : IInjectionTarget
{
    IInjectionTarget realTarget { get; }
}


