// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using System;

namespace ApiPathAnalyzer.Sample;

// If you don't see warnings, build the Analyzers Project.

public class Examples
{
    public class MyCompanyClass // Try to apply quick fix using the IDE.
    {
    }
    
    [Route("/xapi/spaceships-hello")]
    public void ToStars()
    {
        
    }
}

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class RouteAttribute : Attribute
{
    public RouteAttribute()
    {
        
    }
    public RouteAttribute(string apiSpaceshipsHello)
    {
        throw new NotImplementedException();
    }
}