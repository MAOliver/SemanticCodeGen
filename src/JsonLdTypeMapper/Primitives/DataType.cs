using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonLdTypeMapper.Primitives
{
    public class DataType<T> : CodePrimitiveExpression where T : struct
    {
        public DataType( )
        {
        }

        public DataType( T value )
            : base( value )
        {
        }

        public new T? Value
        {
            get { return base.Value as T?; }
        }
    }
}
