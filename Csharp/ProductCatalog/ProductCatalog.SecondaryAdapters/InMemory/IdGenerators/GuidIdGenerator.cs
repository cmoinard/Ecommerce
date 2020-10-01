using System;

namespace ProductCatalog.SecondaryAdapters.InMemory.IdGenerators
{
    internal class GuidIdGenerator : IIdGenerator<Guid>
    {
        public Guid NewId() => Guid.NewGuid();
    }
}