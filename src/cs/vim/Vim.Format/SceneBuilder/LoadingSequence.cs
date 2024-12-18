using System.Linq;

namespace Vim.Format.SceneBuilder
{
    public class LoadingSequence : ILoadingStep
    {
        private readonly LoadingStep[] _steps;
        public float Effort { get; }

        public LoadingSequence(params LoadingStep[] steps)
        {
            _steps = steps;
            Effort = _steps.Sum(s => s.Effort);
        }

        public void Run(ILoadingProgress progress)
        {
            foreach (var step in _steps)
                step.Run(progress);
        }
    }
}
