namespace Vim.Format.ObjectModel
{
    public readonly struct GetOrAddResult<TEntity> where TEntity : Entity
    {
        public readonly TEntity Entity;
        public readonly bool Added;

        public GetOrAddResult(TEntity entity, bool added)
            => (Entity, Added) = (entity, added);

        public static GetOrAddResult<TEntity> Empty
            => new GetOrAddResult<TEntity>(null, false);

        public bool IsEmpty
            => Entity == null;

        public void Deconstruct(out TEntity entity, out bool added)
            => (entity, added) = (Entity, Added);
    }
}
