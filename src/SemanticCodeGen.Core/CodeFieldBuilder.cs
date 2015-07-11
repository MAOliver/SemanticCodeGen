using System;
using System.CodeDom;

namespace SchemaSpider.Core
{
    public class CodeFieldBuilder
    {
        private readonly Type _type;
        private readonly string _name;
        private readonly CodeAttributeDeclaration[ ] _caDeclarations;
        private readonly string[ ] _comments;
        private readonly MemberAttributes? _memberAttributes;

        private CodeFieldBuilder( Type type, string name, CodeAttributeDeclaration[ ] caDeclarations = null, MemberAttributes? memberAttributes = null, string[ ] comments = null )
        {
            _type = type;
            _name = name;
            _caDeclarations = caDeclarations ?? new CodeAttributeDeclaration[ 0 ];
            _comments = comments ?? new string[ 0 ];
            _memberAttributes = memberAttributes;
        }

        public static CodeFieldBuilder New<T>( string name )
        {
            return new CodeFieldBuilder( typeof( T ), name );
        }

        public CodeFieldBuilder AddAttributes( MemberAttributes memberAttributes )
        {
            return new CodeFieldBuilder( _type, _name, caDeclarations: _caDeclarations, memberAttributes: memberAttributes, comments: _comments );
        }

        public CodeFieldBuilder AddCustomAttributes( params CodeAttributeDeclaration[ ] caDeclarations )
        {
            return new CodeFieldBuilder( _type, _name, caDeclarations: caDeclarations, memberAttributes: _memberAttributes, comments: _comments );
        }

        public CodeFieldBuilder AddComments( params string[ ] comments )
        {
            return new CodeFieldBuilder( _type, _name, caDeclarations: _caDeclarations, memberAttributes: _memberAttributes, comments: comments );
        }

        public CodeMemberField Build( CodeExpression initialization = null )
        {
            var cmf = new CodeMemberField( _type, _name );
            cmf.Comments.AddAllNonNull( cm => new CodeCommentStatement( cm ), _comments );
            cmf.Attributes = _memberAttributes.GetValueOrDefault( MemberAttributes.Private | MemberAttributes.Final );
            cmf.CustomAttributes.AddAllNonNull( ca => ca, _caDeclarations );
            cmf.InitExpression = initialization ?? cmf.InitExpression;
            return cmf;
        }
    }
}