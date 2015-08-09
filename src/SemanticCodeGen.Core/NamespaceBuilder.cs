using System.CodeDom;
using SchemaSpider.Core.Extensions;

namespace SchemaSpider.Core
{
    public class NamespaceBuilder
    {
        private readonly CodeTypeBuilder[] _codeTypeBuilders;
        //private readonly CodeNamespace _namespace;
        private readonly string _ns;
        private readonly string[ ] _imports;
        private readonly string[ ] _comments;

        public static NamespaceBuilder New( string @namespace )
        {
            return new NamespaceBuilder( @namespace );
        }

        private NamespaceBuilder( string ns, string[ ] imports = null, string[ ] comments = null, CodeTypeBuilder[ ] codeTypeBuilders = null )
        {
            _ns = ns ?? "";
            _imports = imports ?? new string[ 0 ];
            _comments = comments ?? new string[ 0 ];
            _codeTypeBuilders = codeTypeBuilders ?? new CodeTypeBuilder[0];

        }

        public NamespaceBuilder AddImport( params string[ ] imports )
        {
            return new NamespaceBuilder( _ns, imports, _comments, _codeTypeBuilders );
        }

        public NamespaceBuilder AddComments( params string[ ] comments )
        {
            return new NamespaceBuilder( _ns, _imports, comments, _codeTypeBuilders );
        }

        public NamespaceBuilder AddCodeTypes( params CodeTypeBuilder[ ] codeTypeBuilders )
        {
            return new NamespaceBuilder( _ns, _imports, _comments, codeTypeBuilders );
        }

        public CodeNamespace Build( )
        {
            var cns = new CodeNamespace( _ns );
            cns.Imports.AddAllNonBlank( s => new CodeNamespaceImport( s ), _imports );
            cns.Comments.AddAllNonBlank( s => new CodeCommentStatement( s ), _comments );
            cns.Types.AddAllNonNull( ct => ct.Build( ), _codeTypeBuilders );
            return cns;
        }

    }
}
