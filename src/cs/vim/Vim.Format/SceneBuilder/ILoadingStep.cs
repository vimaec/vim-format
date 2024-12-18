namespace Vim.Format.SceneBuilder
{
    using System;

    public class LoadingProgress: Progress<(string, double)>, ILoadingProgress
    {
        public LoadingProgress(Action<(string, double)> handler) : base(handler)
        {
        }
    }

    public interface ILoadingProgress : IProgress<(string, double)>
    {
    }

    public interface ILoadingStep
    {
        void Run(ILoadingProgress progress);
        float Effort { get; }
    }
}
