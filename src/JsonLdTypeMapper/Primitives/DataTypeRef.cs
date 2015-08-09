using System.CodeDom;

namespace JsonLdTypeMapper.Primitives
{
    public class DataTypeRef<T> : CodePrimitiveExpression where T : class
    {
        public DataTypeRef( )
        {
        }

        public DataTypeRef( T value )
            : base( value )
        {
        }

        public new T Value
        {
            get { return base.Value as T; }
        }
    }
}