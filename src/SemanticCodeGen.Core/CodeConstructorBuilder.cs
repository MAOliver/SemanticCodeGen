using System.CodeDom;

namespace SchemaSpider.Core
{
    public class CodeConstructorBuilder
    {
        public static CodeConstructorBuilder New( )
        {
            return new CodeConstructorBuilder( );
        }

        public CodeConstructor Build( )
        {
            var cc =  new CodeConstructor( );
            return cc;
        }
    }

}