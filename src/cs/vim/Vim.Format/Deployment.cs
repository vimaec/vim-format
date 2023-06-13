using System;
using System.Runtime.CompilerServices;

// Resharper disable once CheckNamespace
namespace Vim.Format
{
    /// <summary>
    /// The deployment types we currently support.
    /// </summary>
    public enum DeploymentTarget
    {
        Development,
        Testing,
        Staging,
        Production,
    }

    public class DeploymentTargetNotSupportedException : Exception
    {
        public readonly DeploymentTarget Target;
        public readonly string Caller;

        public DeploymentTargetNotSupportedException(DeploymentTarget target, [CallerMemberName] string caller = null)
            : base($"Deployment target {target:G} not supported in '{caller ?? ""}'")
        {
            Target = target;
            Caller = caller;
        }
    }

    /// <summary>
    /// A deployment represents a collection of endpoints which vary according to the targeted deployment environment.
    /// </summary>
    public class Deployment
    {
        public readonly Uri Production;
        public readonly Uri Staging;
        public readonly Uri Testing;
        public readonly Uri Development;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Deployment(
            Uri production,
            Uri staging,
            Uri testing,
            Uri development)
        {
            Production = production;
            Staging = staging;
            Testing = testing;
            Development = development;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Deployment(
            string production,
            string staging,
            string testing,
            string development)
        : this(
            new Uri(production),
            new Uri(staging),
            new Uri(testing),
            new Uri(development))
        { }

        /// <summary>
        /// Returns the URI corresponding to the given deployment type.
        /// </summary>
        public Uri GetUri(DeploymentTarget deploymentTarget)
        {
            switch (deploymentTarget)
            {
                case DeploymentTarget.Production:
                    return Production;
// NOTE: Public releases cannot access staging, testing, or development targets.
#if !VIM_RELEASE_TYPE_PUBLIC
                case DeploymentTarget.Staging:
                    return Staging;
                case DeploymentTarget.Testing:
                    return Testing;
                case DeploymentTarget.Development:
                    return Development;
#endif
                default:
                    throw new DeploymentTargetNotSupportedException(deploymentTarget);
            }
        }
    }
}
