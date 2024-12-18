using System;

namespace Vim.Format.SceneBuilder
{
    public class LoadingStep : ILoadingStep
    {
        private readonly Action _action;
        private readonly string _name;
        public float Effort { get; }

        public LoadingStep(Action action, string name, float effort = 1f)
        {
            _action = action;
            _name = name;
            Effort = effort;
        }

        public void Run(ILoadingProgress progress)
        {
            progress?.Report((_name, Effort));
            _action();
        }
    }
}
