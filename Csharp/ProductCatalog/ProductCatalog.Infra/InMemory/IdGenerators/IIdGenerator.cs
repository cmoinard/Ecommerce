namespace ProductCatalog.Infra.InMemory.IdGenerators
{
    internal interface IIdGenerator<TId>
    {
        TId NewId();
    }
}