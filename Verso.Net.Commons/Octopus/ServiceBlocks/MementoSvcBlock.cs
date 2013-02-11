using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Verso.Net.Commons.Octopus.ServiceBlocks
{
    public class MementoSvcBlock : BaseSvcBlock
    {
        public enum Verb
        {
            PersistEntity,
            UpdateEntity,
            DeleteEntity,
            GetEntity,
            GetEntities,
            GetEntitiesDs
        }

        internal override void InitializeSvcBlock()
        {
            ServiceBlockName = "Memento";
        }
    }
}
