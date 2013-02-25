using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Verso.Net.Commons.Octopus.ServiceBlocks
{
    public abstract class BaseSvcBlock
    {
        public string ServiceBlockName;

        protected BaseSvcBlock()
        {
            InitializeSvcBlock();
        }

        internal abstract void InitializeSvcBlock();

        public virtual VersoMsg GetVerso(Enum verb)
        {
            var verso = new VersoMsg();

            verso.ServiceBlock = ServiceBlockName;
            verso.Verb = verb.ToString();

            return verso;
        }

        public virtual VersoMsg GetVerso<T>(Enum verb, object dataVerso)
        {
            var verso = new VersoMsg();

            verso.ServiceBlock = ServiceBlockName;
            verso.Verb = verb.ToString();
            verso.SetData<T>(dataVerso);

            return verso;
        }
    }
}
