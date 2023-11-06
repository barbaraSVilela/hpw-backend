using System;
using Microsoft.Azure.WebJobs.Description;

namespace HPW.Bindings.Attributes{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    [Binding]
    public sealed class AuthTokenAttribute : System.Attribute
    { }
}