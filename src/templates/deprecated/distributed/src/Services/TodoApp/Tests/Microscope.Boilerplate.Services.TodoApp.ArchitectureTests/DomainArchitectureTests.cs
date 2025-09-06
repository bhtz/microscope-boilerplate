using Microscope.Boilerplate.Services.TodoApp.Domain;
using Microscope.SharedKernel;
using NetArchTest.Rules;

namespace Microscope.Boilerplate.Services.TodoApp.ArchitectureTests;

public class DomainArchitectureTests
{
    private const string SharedKernelNameSpace = "Microscope.SharedKernel";
    private const string DomainNameSpace = "Microscope.Boilerplate.Services.TodoApp.Domain";
    
    [Fact]
    public void CheckDomainNameSpace()
    {
        var assembly = typeof(ITodoAppDomainModule).Assembly;

        var res = Types.InAssembly(assembly)
            .Should()
            .ResideInNamespace(DomainNameSpace)
            .GetResult();
        
        Assert.True(res.IsSuccessful);
    }
    
    [Fact]
    public void DomainShouldNotHaveDependenciesOtherThanSharedKernel()
    {
        var assembly = typeof(ITodoAppDomainModule).Assembly;
        
        var ns = new string[]
        {
            SharedKernelNameSpace,
            DomainNameSpace
        };
        
        // Todo
    }
    
    [Fact]
    public void RepositoryNamingConvention()
    {
        var assembly = typeof(ITodoAppDomainModule).Assembly;
        
        var res = Types.InAssembly(assembly)
            .That()
            .Inherit(typeof(IRepository))
            .Should()
            .HaveNameEndingWith("Repository")
            .GetResult();
        
        Assert.True(res.IsSuccessful);
    }
    
    [Fact]
    public void InterfaceNamingConvention()
    {
        var assembly = typeof(ITodoAppDomainModule).Assembly;
        
        var res = Types.InAssembly(assembly)
            .That()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();
        
        Assert.True(res.IsSuccessful);
    }
    
    [Fact]
    public void AuditableAggregateRootConvention()
    {
        var assembly = typeof(ITodoAppDomainModule).Assembly;
        
        var res = Types.InAssembly(assembly)
            .That()
            .Inherit(typeof(IAggregateRoot))
            .Should()
            .Inherit(typeof(AuditableEntity<Guid>))
            .GetResult();
        
        Assert.True(res.IsSuccessful);
    }
}