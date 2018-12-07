using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Indieteur.VDFAPI;

namespace Demo
{
    class TreeNodeVDFTag
    {
        public enum Type
        {
            Node,
            Key
        }
        public Type TagType { get; private set; }
        public BaseToken Token { get; private set; }
        public TreeNodeVDFTag(Type tagType, BaseToken _Token)
        {
            TagType = tagType;
            Token = _Token ?? throw new NullReferenceException("Object property of TreeNodeVDFTag cannot be null!");
        }

        
    }
}
