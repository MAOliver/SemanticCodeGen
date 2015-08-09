using System;
using System.CodeDom;
using SchemaSpider.Core.Extensions;

namespace SchemaSpider.Core
{
    public class CodePropertyBuilder
    {
        private readonly Type _type;
        private readonly string _name;
        private readonly bool? _hasGet;
        private readonly bool? _hasSet;
        private readonly CodeMethodReturnStatement _getStatement;
        private readonly CodeAssignStatement _setStatement;
        private readonly CodeAttributeDeclaration[ ] _caDeclarations;
        private readonly string[ ] _comments;
        private readonly MemberAttributes? _memberAttributes;
        private readonly CodeParameterDeclarationExpression _paramExpression;

        private CodePropertyBuilder
        (
            Type type
            , string name
            , bool? hasGet = null
            , bool? hasSet = null
            , CodeMethodReturnStatement getStatement = null
            , CodeAssignStatement setStatement = null
            , CodeAttributeDeclaration[ ] caDeclarations = null
            , MemberAttributes? memberAttributes = null
            , CodeParameterDeclarationExpression paramExpression = null
            , string[ ] comments = null

        )
        {
            _type = type;
            _name = name;
            _hasGet = hasGet;
            _hasSet = hasSet;
            _getStatement = getStatement;
            _setStatement = setStatement;
            _caDeclarations = caDeclarations ?? new CodeAttributeDeclaration[ 0 ];
            _comments = comments ?? new string[ 0 ];
            _memberAttributes = memberAttributes;
            _paramExpression = paramExpression;
        }

        public static CodePropertyBuilder New<T>( string name )
        {
            return new CodePropertyBuilder( typeof( T ), name );
        }

        public CodePropertyBuilder AddAttributes( MemberAttributes memberAttributes )
        {
            return new CodePropertyBuilder( _type, _name, hasGet: _hasGet, hasSet: _hasSet, getStatement: _getStatement, setStatement: _setStatement, caDeclarations: _caDeclarations, memberAttributes: memberAttributes, paramExpression: _paramExpression, comments: _comments );
        }

        public CodePropertyBuilder AddCustomAttributes( params CodeAttributeDeclaration[ ] caDeclarations )
        {
            return new CodePropertyBuilder( _type, _name, hasGet: _hasGet, hasSet: _hasSet, getStatement: _getStatement, setStatement: _setStatement, caDeclarations: caDeclarations, memberAttributes: _memberAttributes, paramExpression: _paramExpression, comments: _comments );
        }

        public CodePropertyBuilder AddComments( params string[ ] comments )
        {
            return new CodePropertyBuilder( _type, _name, hasGet: _hasGet, hasSet: _hasSet, getStatement: _getStatement, setStatement: _setStatement, caDeclarations: _caDeclarations, memberAttributes: _memberAttributes, paramExpression: _paramExpression, comments: comments );
        }

        public CodePropertyBuilder AddGet( CodeMethodReturnStatement getStatement = null )
        {
            return new CodePropertyBuilder( _type, _name, hasGet: _hasGet, hasSet: _hasSet, getStatement: getStatement, setStatement: _setStatement, caDeclarations: _caDeclarations, memberAttributes: _memberAttributes, paramExpression: _paramExpression, comments: _comments );
        }

        public CodePropertyBuilder AddSet( CodeAssignStatement setStatement = null )
        {
            return new CodePropertyBuilder( _type, _name, hasGet: _hasGet, hasSet: _hasSet, getStatement: _getStatement, setStatement: setStatement, caDeclarations: _caDeclarations, memberAttributes: _memberAttributes, paramExpression: _paramExpression, comments: _comments );
        }

        public CodePropertyBuilder AddParameterExpression( CodeParameterDeclarationExpression paramExpression )
        {
            return new CodePropertyBuilder( _type, _name, hasGet: _hasGet, hasSet: _hasSet, getStatement: _getStatement, setStatement: _setStatement, caDeclarations: _caDeclarations, memberAttributes: _memberAttributes, paramExpression: paramExpression, comments: _comments );
        }

        public CodeMemberProperty Build(  )
        {
            var cmf = new CodeMemberProperty
            {
                Name = _name,
                Type = new CodeTypeReference( _type ),
                Attributes = _memberAttributes.GetValueOrDefault( ),
                HasGet = _hasGet.GetValueOrDefault( _getStatement != null ),
                HasSet = _hasSet.GetValueOrDefault( _setStatement != null )
            };
            //MemberAttributes.Private | MemberAttributes.Final );
            cmf.GetStatements.AddAllNonNull( gs => gs, _getStatement );
            cmf.SetStatements.AddAllNonNull( ss => ss, _setStatement );
            cmf.Comments.AddAllNonNull( cm => new CodeCommentStatement( cm ), _comments );
            cmf.CustomAttributes.AddAllNonNull( ca => ca, _caDeclarations );
            cmf.Parameters.AddAllNonNull(exp=>exp, _paramExpression);
            return cmf;
        }

    }
}
