using System;
using System.CodeDom;

namespace SchemaSpider.Core
{
    public class CodeMemberMethodBuilder
    {
        public static CodeMemberMethodBuilder New( )
        {
            return new CodeMemberMethodBuilder( );
        }

        public CodeMemberMethod Build( )
        {
            return new CodeMemberMethod( );
        }
    }
}
