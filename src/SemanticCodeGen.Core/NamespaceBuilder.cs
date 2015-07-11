using System.CodeDom;

namespace SchemaSpider.Core
{
    public class NamespaceBuilder
    {
        //private readonly CodeNamespace _namespace;
        private readonly string _ns;
        private readonly string[ ] _imports;
        private readonly string[ ] _comments;

        public static NamespaceBuilder New( string @namespace )
        {
            return new NamespaceBuilder( @namespace, null, null );
        }

        private NamespaceBuilder( string ns, string[ ] imports, string[ ] comments )
        {
            _ns = ns ?? "";
            _imports = imports ?? new string[ 0 ];
            _comments = comments ?? new string[ 0 ];
        }

        public NamespaceBuilder AddImport( params string[ ] imports )
        {
            return new NamespaceBuilder( _ns, imports, _comments );
        }

        public NamespaceBuilder AddComments( params string[ ] comments )
        {
            return new NamespaceBuilder( _ns, _imports, comments );
        }

        public CodeNamespace Build( params CodeTypeBuilder[ ] codeTypeBuilders )
        {
            var cns = new CodeNamespace( _ns );
            cns.Imports.AddAllNonBlank( s => new CodeNamespaceImport( s ), _imports );
            cns.Comments.AddAllNonBlank( s => new CodeCommentStatement( s ), _comments );
            cns.Types.AddAllNonNull( ct => ct.Build( ), codeTypeBuilders );
            return cns;
        }

    }
}
