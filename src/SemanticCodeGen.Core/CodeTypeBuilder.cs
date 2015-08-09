using System.CodeDom;
using System.Reflection;
using SchemaSpider.Core.Extensions;

namespace SchemaSpider.Core
{
    public class CodeTypeBuilder
    {
        private readonly string _name;
        private readonly TypeAttributes? _typeAttributes;
        private readonly MemberAttributes? _memberAttributes;
        private readonly CodeConstructorBuilder[ ] _codeConstructorBuilders;
        private readonly CodePropertyBuilder[ ] _codePropertyBuilders;
        private readonly CodeMemberMethodBuilder[ ] _memberMethodBuilders;
        private readonly CodeTypeReference[ ] _ctReferences;
        private readonly CodeTypeParameter[ ] _ctParameters;
        private readonly CodeAttributeDeclaration[ ] _caDeclarations;
        private readonly CodeFieldBuilder[ ] _codeFieldBuilders;
        private readonly string[ ] _comments;
        private readonly bool _isClass;
        private readonly bool _isPartial;
        private readonly bool _isEnum;
        private readonly bool _isStruct;
        private readonly bool _isInterface;

        private CodeTypeBuilder
            (
            string name
            , TypeAttributes? typeAttributes = null
            , MemberAttributes? memberAttributes = null
            , CodeTypeReference[ ] ctReferences = null
            , CodeTypeParameter[ ] ctParameters = null
            , CodeAttributeDeclaration[ ] caDeclarations = null
            , CodeFieldBuilder[ ] codeFieldBuilders = null
            , CodeMemberMethodBuilder[ ] memberMethodBuilders = null
            , CodeConstructorBuilder[ ] codeConstructorBuilders = null
            , CodePropertyBuilder[ ] codePropertyBuilders = null
            , string[ ] comments = null
            , bool isClass = false
            , bool isPartial = false
            , bool isEnum = false
            , bool isStruct = false
            , bool isInterface = false
            )
        {
            _name = name;
            _typeAttributes = typeAttributes;
            _memberAttributes = memberAttributes;
            _codeConstructorBuilders = codeConstructorBuilders;
            _codePropertyBuilders = codePropertyBuilders ?? new CodePropertyBuilder[ 0 ];
            _memberMethodBuilders = memberMethodBuilders ?? new CodeMemberMethodBuilder[ 0 ];
            _ctReferences = ctReferences ?? new CodeTypeReference[ 0 ];
            _ctParameters = ctParameters ?? new CodeTypeParameter[ 0 ];
            _caDeclarations = caDeclarations ?? new CodeAttributeDeclaration[ 0 ];
            _codeFieldBuilders = codeFieldBuilders ?? new CodeFieldBuilder[ 0 ];
            _comments = comments ?? new string[ 0 ];
            _isClass = isClass;
            _isPartial = isPartial;
            _isEnum = isEnum;
            _isStruct = isStruct;
            _isInterface = isInterface;
        }

        public static CodeTypeBuilder NewClass( string name, bool isPartial = false )
        {
            return new CodeTypeBuilder( name, isClass: true, isPartial: isPartial );
        }

        public static CodeTypeBuilder NewInterface( string name )
        {
            return new CodeTypeBuilder( name, isInterface: true );
        }

        public static CodeTypeBuilder NewEnum( string name )
        {
            return new CodeTypeBuilder( name, isEnum: true );
        }

        public static CodeTypeBuilder NewStruct( string name )
        {
            return new CodeTypeBuilder( name, isStruct: true );
        }

        public CodeTypeBuilder AddTypeAttributes( TypeAttributes? attributes )
        {
            return new CodeTypeBuilder( _name, _typeAttributes, _memberAttributes, _ctReferences, _ctParameters, _caDeclarations, _codeFieldBuilders, _memberMethodBuilders, _codeConstructorBuilders, _codePropertyBuilders, _comments, _isClass, _isPartial, _isEnum, _isStruct, _isInterface );
        }

        public CodeTypeBuilder AddBaseType( params CodeTypeReference[ ] ctReferences )
        {
            return new CodeTypeBuilder( _name, _typeAttributes, _memberAttributes, ctReferences, _ctParameters, _caDeclarations, _codeFieldBuilders, _memberMethodBuilders, _codeConstructorBuilders, _codePropertyBuilders, _comments, _isClass, _isPartial, _isEnum, _isStruct, _isInterface );
        }

        public CodeTypeBuilder AddTypeParameters( params CodeTypeParameter[ ] ctParameters )
        {
            return new CodeTypeBuilder( _name, _typeAttributes, _memberAttributes, _ctReferences, ctParameters, _caDeclarations, _codeFieldBuilders, _memberMethodBuilders, _codeConstructorBuilders, _codePropertyBuilders, _comments, _isClass, _isPartial, _isEnum, _isStruct, _isInterface );
        }

        public CodeTypeBuilder AddAttributes( MemberAttributes memberAttributes )
        {
            return new CodeTypeBuilder( _name, _typeAttributes, memberAttributes, _ctReferences, _ctParameters, _caDeclarations, _codeFieldBuilders, _memberMethodBuilders, _codeConstructorBuilders, _codePropertyBuilders, _comments, _isClass, _isPartial, _isEnum, _isStruct, _isInterface );
        }

        public CodeTypeBuilder AddCustomAttributes( params CodeAttributeDeclaration[ ] customAttributes )
        {
            return new CodeTypeBuilder( _name, _typeAttributes, _memberAttributes, _ctReferences, _ctParameters, customAttributes, _codeFieldBuilders, _memberMethodBuilders, _codeConstructorBuilders, _codePropertyBuilders, _comments, _isClass, _isPartial, _isEnum, _isStruct, _isInterface );
        }

        public CodeTypeBuilder AddComments( params string[ ] comments )
        {
            return new CodeTypeBuilder( _name, _typeAttributes, _memberAttributes, _ctReferences, _ctParameters, _caDeclarations, _codeFieldBuilders, _memberMethodBuilders, _codeConstructorBuilders, _codePropertyBuilders, comments, _isClass, _isPartial, _isEnum, _isStruct, _isInterface );
        }

        public CodeTypeBuilder AddField( params CodeFieldBuilder[ ] codeFieldBuilders )
        {
            return new CodeTypeBuilder( _name, _typeAttributes, _memberAttributes, _ctReferences, _ctParameters, _caDeclarations, codeFieldBuilders, _memberMethodBuilders, _codeConstructorBuilders, _codePropertyBuilders, _comments, _isClass, _isPartial, _isEnum, _isStruct, _isInterface );
        }

        public CodeTypeBuilder AddMethod( params CodeMemberMethodBuilder[ ] memberMethodBuilders )
        {
            return new CodeTypeBuilder( _name, _typeAttributes, _memberAttributes, _ctReferences, _ctParameters, _caDeclarations, _codeFieldBuilders, memberMethodBuilders, _codeConstructorBuilders, _codePropertyBuilders, _comments, _isClass, _isPartial, _isEnum, _isStruct, _isInterface );
        }

        public CodeTypeBuilder AddConstructor( params CodeConstructorBuilder[ ] codeConstructorBuilders )
        {
            return new CodeTypeBuilder( _name, _typeAttributes, _memberAttributes, _ctReferences, _ctParameters, _caDeclarations, _codeFieldBuilders, _memberMethodBuilders, codeConstructorBuilders, _codePropertyBuilders, _comments, _isClass, _isPartial, _isEnum, _isStruct, _isInterface );
        }

        public CodeTypeBuilder AddProperties( params CodePropertyBuilder[ ] codePropertyBuilders )
        {
            return new CodeTypeBuilder( _name, _typeAttributes, _memberAttributes, _ctReferences, _ctParameters, _caDeclarations, _codeFieldBuilders, _memberMethodBuilders, _codeConstructorBuilders, codePropertyBuilders, _comments, _isClass, _isPartial, _isEnum, _isStruct, _isInterface );
        }

        public CodeTypeDeclaration Build( )
        {
            var ctd = new CodeTypeDeclaration( _name );
            ctd.TypeAttributes = _typeAttributes.GetValueOrDefault( );
            ctd.BaseTypes.AddAllNonNull( ct => ct, _ctReferences );
            ctd.IsClass = _isClass;
            ctd.IsEnum = _isEnum;
            ctd.IsInterface = _isInterface;
            ctd.IsPartial = _isPartial;
            ctd.Members.AddAllNonNull( mmb => mmb.Build( ), _memberMethodBuilders );
            ctd.Members.AddAllNonNull( cfb => cfb.Build( ), _codeFieldBuilders );
            ctd.Members.AddAllNonNull( pb => pb.Build( ), _codePropertyBuilders );
            ctd.Members.AddAllNonNull( cc => cc.Build( ), _codeConstructorBuilders );
            ctd.Attributes = _memberAttributes.GetValueOrDefault( );
            ctd.TypeParameters.AddAllNonNull( tp => tp, _ctParameters );
            ctd.CustomAttributes.AddAllNonNull( cta => cta, _caDeclarations );
            return ctd;
        }
    }
}