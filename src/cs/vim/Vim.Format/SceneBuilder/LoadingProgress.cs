namespace Vim.Format.SceneBuilder
{
    public class CumulativeProgressDecorator : ILoadingProgress
    {
        private readonly double _total;
        private double _current;

        private readonly ILoadingProgress _progress;

        private CumulativeProgressDecorator(ILoadingProgress progress, float total)
            => (_progress, _total) = (progress, total);

        public static CumulativeProgressDecorator Decorate(ILoadingProgress logger, float total)
            => logger != null ? new CumulativeProgressDecorator(logger, total) : null;

        public void Report((string, double) value)
        {
            _current += value.Item2;
            _progress.Report((value.Item1, _current / _total));
        }
    }
}
