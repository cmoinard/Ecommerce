namespace ProductCatalog.SecondaryAdapters.InMemory.IdGenerators
{
    internal class IntIdGenerator : IIdGenerator<int>
    {
        private int _lastId;
        public int NewId() => ++_lastId;
    }
}