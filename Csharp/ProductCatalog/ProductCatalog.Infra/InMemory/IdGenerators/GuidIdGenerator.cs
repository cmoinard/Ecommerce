using System;

namespace ProductCatalog.Infra.InMemory.IdGenerators
{
    internal class GuidIdGenerator : IIdGenerator<Guid>
    {
        public Guid NewId() => Guid.NewGuid();
    }
}