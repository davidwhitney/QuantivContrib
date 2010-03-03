using System;

namespace Quantiv.Runtime
{
    public class EntityManager
    {
        public EntityRetriever CreateEntityRetriever()
        {
            return new EntityRetriever();
        }

        public Entity CreateEntity()
        {
            return new Entity();
        }

        public EntityListRetriever CreateEntityListRetriever()
        {
            return new EntityListRetriever();
        }
    }
}