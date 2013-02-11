using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Verso.Net.Commons.Octopus.ServiceBlocks
{
    public class OctopusSvcBlock : BaseSvcBlock
    {
        public enum Verb
        {
            AddWorks,
            RemoveWork,
            UpdateWork
        }

        internal override void InitializeSvcBlock()
        {
            ServiceBlockName = "Octopus";
        }
    }
}
