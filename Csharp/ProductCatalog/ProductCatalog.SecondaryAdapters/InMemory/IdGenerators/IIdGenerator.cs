namespace ProductCatalog.SecondaryAdapters.InMemory.IdGenerators
{
    internal interface IIdGenerator<TId>
    {
        TId NewId();
    }
}