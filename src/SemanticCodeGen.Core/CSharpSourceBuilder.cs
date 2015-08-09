using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using SchemaSpider.Core.Extensions;

namespace SchemaSpider.Core
{
    /// <summary> 
    /// This code example creates a graph using a CodeCompileUnit and   
    /// generates source code for the graph using the CSharpCodeProvider. 
    /// </summary> 
    public class CSharpSourceBuilder
    {
        private readonly NamespaceBuilder[] _namespaces;

        protected CSharpSourceBuilder(NamespaceBuilder[] namespaces = null)
        {
            _namespaces = namespaces ?? new NamespaceBuilder[0];
        }

        public static CSharpSourceBuilder New()
        {
            return new CSharpSourceBuilder( );
        }

        public CSharpSourceBuilder AddNamespaceCollection(params NamespaceBuilder[] namespaces)
        {
            return new CSharpSourceBuilder( namespaces );
        }

        //public virtual CSharpSourceBuilder AddClass( CodeTypeBuilder builder )
       
        /// <summary> 
        /// Generate CSharp source code from the compile unit. 
        /// </summary> 
        public string Build( )
        {
            var codeCompileUnit = new CodeCompileUnit();
            codeCompileUnit.Namespaces.AddAllNonNull(ns=>ns.Build(), _namespaces);
            CodeDomProvider provider = CodeDomProvider.CreateProvider( "CSharp" );
            CodeGeneratorOptions options = new CodeGeneratorOptions {BracingStyle = "C"};
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                provider.GenerateCodeFromCompileUnit(
                    codeCompileUnit, sw, options );
            }
            return sb.ToString();
        }
    }


}

/*/// <summary> 
        /// Adds two fields to the class. 
        /// </summary> 
        public void AddFields( )
        {
            // Declare the widthValue field.
            /*CodeMemberField widthValueField = new CodeMemberField( );
            widthValueField.Attributes = MemberAttributes.Private;
            widthValueField.Name = "widthValue";
            widthValueField.Type = new CodeTypeReference( typeof( System.Double ) );
            widthValueField.Comments.Add( new CodeCommentStatement(
                "The width of the object." ) );
            targetClass.Members.Add( widthValueField );

            // Declare the heightValue field
            CodeMemberField heightValueField = new CodeMemberField( );
            heightValueField.Attributes = MemberAttributes.Private;
            heightValueField.Name = "heightValue";
            heightValueField.Type =
                new CodeTypeReference( typeof( System.Double ) );
            heightValueField.Comments.Add( new CodeCommentStatement(
                "The height of the object." ) );
            targetClass.Members.Add( heightValueField );#1#
        }
        /// <summary> 
        /// Add three properties to the class. 
        /// </summary> 
        public void AddProperties( )
        {
            // Declare the read-only Width property.
            /*CodeMemberProperty widthProperty = new CodeMemberProperty( );
            widthProperty.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;
            widthProperty.Name = "Width";
            widthProperty.HasGet = true;
            widthProperty.Type = new CodeTypeReference( typeof( System.Double ) );
            widthProperty.Comments.Add( new CodeCommentStatement(
                "The Width property for the object." ) );
            widthProperty.GetStatements.Add( new CodeMethodReturnStatement(
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression( ), "widthValue" ) ) );
            targetClass.Members.Add( widthProperty );

            // Declare the read-only Height property.
            CodeMemberProperty heightProperty = new CodeMemberProperty( );
            heightProperty.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;
            heightProperty.Name = "Height";
            heightProperty.HasGet = true;
            heightProperty.Type = new CodeTypeReference( typeof( System.Double ) );
            heightProperty.Comments.Add( new CodeCommentStatement(
                "The Height property for the object." ) );
            heightProperty.GetStatements.Add( new CodeMethodReturnStatement(
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression( ), "heightValue" ) ) );
            targetClass.Members.Add( heightProperty );

            // Declare the read only Area property.
            CodeMemberProperty areaProperty = new CodeMemberProperty( );
            areaProperty.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;
            areaProperty.Name = "Area";
            areaProperty.HasGet = true;
            areaProperty.Type = new CodeTypeReference( typeof( System.Double ) );
            areaProperty.Comments.Add( new CodeCommentStatement(
                "The Area property for the object." ) );

            // Create an expression to calculate the area for the get accessor  
            // of the Area property.
            CodeBinaryOperatorExpression areaExpression =
                new CodeBinaryOperatorExpression(
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression( ), "widthValue" ),
                CodeBinaryOperatorType.Multiply,
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression( ), "heightValue" ) );
            areaProperty.GetStatements.Add(
                new CodeMethodReturnStatement( areaExpression ) );
            targetClass.Members.Add( areaProperty );#1#
        }

        /// <summary> 
        /// Adds a method to the class. This method multiplies values stored  
        /// in both fields. 
        /// </summary> 
        public void AddMethod( )
        {
            // Declaring a ToString method
            /*CodeMemberMethod toStringMethod = new CodeMemberMethod( );
            toStringMethod.Attributes =
                MemberAttributes.Public | MemberAttributes.Override;
            toStringMethod.Name = "ToString";
            toStringMethod.ReturnType =
                new CodeTypeReference( typeof( System.String ) );

            CodeFieldReferenceExpression widthReference =
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression( ), "Width" );
            CodeFieldReferenceExpression heightReference =
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression( ), "Height" );
            CodeFieldReferenceExpression areaReference =
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression( ), "Area" );

            // Declaring a return statement for method ToString.
            CodeMethodReturnStatement returnStatement =
                new CodeMethodReturnStatement( );

            // This statement returns a string representation of the width, 
            // height, and area. 
            string formattedOutput = "The object:" + Environment.NewLine +
                " width = {0}," + Environment.NewLine +
                " height = {1}," + Environment.NewLine +
                " area = {2}";
            returnStatement.Expression =
                new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression( "System.String" ), "Format",
                new CodePrimitiveExpression( formattedOutput ),
                widthReference, heightReference, areaReference );
            toStringMethod.Statements.Add( returnStatement );
            targetClass.Members.Add( toStringMethod );#1#
        }
        /// <summary> 
        /// Add a constructor to the class. 
        /// </summary> 
        public void AddConstructor( )
        {
            /#1#/ Declare the constructor
            CodeConstructor constructor = new CodeConstructor( );
            constructor.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;

            // Add parameters.
            constructor.Parameters.Add( new CodeParameterDeclarationExpression(
                typeof( System.Double ), "width" ) );
            constructor.Parameters.Add( new CodeParameterDeclarationExpression(
                typeof( System.Double ), "height" ) );

            // Add field initialization logic
            CodeFieldReferenceExpression widthReference =
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression( ), "widthValue" );
            constructor.Statements.Add( new CodeAssignStatement( widthReference,
                new CodeArgumentReferenceExpression( "width" ) ) );
            CodeFieldReferenceExpression heightReference =
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression( ), "heightValue" );
            constructor.Statements.Add( new CodeAssignStatement( heightReference,
                new CodeArgumentReferenceExpression( "height" ) ) );
            targetClass.Members.Add( constructor );#1#
        }

        /// <summary> 
        /// Add an entry point to the class. 
        /// </summary> 
        public void AddEntryPoint( )
        {
            /*CodeEntryPointMethod start = new CodeEntryPointMethod( );
            CodeObjectCreateExpression objectCreate =
                new CodeObjectCreateExpression(
                new CodeTypeReference( "CodeDOMCreatedClass" ),
                new CodePrimitiveExpression( 5.3 ),
                new CodePrimitiveExpression( 6.9 ) );

            // Add the statement: 
            // "CodeDOMCreatedClass testClass =  
            //     new CodeDOMCreatedClass(5.3, 6.9);"
            start.Statements.Add( new CodeVariableDeclarationStatement(
                new CodeTypeReference( "CodeDOMCreatedClass" ), "testClass",
                objectCreate ) );

            // Creat the expression: 
            // "testClass.ToString()"
            CodeMethodInvokeExpression toStringInvoke =
                new CodeMethodInvokeExpression(
                new CodeVariableReferenceExpression( "testClass" ), "ToString" );

            // Add a System.Console.WriteLine statement with the previous  
            // expression as a parameter.
            start.Statements.Add( new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression( "System.Console" ),
                "WriteLine", toStringInvoke ) );
            targetClass.Members.Add( start );#1#
        }*/
