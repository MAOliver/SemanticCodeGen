using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using JsonLdTypeMapper;
using JsonLD.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace SchemaSpider.Tests
{
    [TestClass]
    public class JsonLdTests
    {
        private Queue<JToken> Queue = new Queue<JToken>( );

        [TestMethod]
        public void ShouldReturnJTokenWhenFileIsPresent( )
        {
            var testFile = "schemaorg_ex.jsonld";
            var file = new JsonLdFile(Path.Combine(Environment.CurrentDirectory, "TestFiles", testFile));

            var token = file.Read();
            RDFDataset ds = JsonLdProcessor.ToRDF(token) as RDFDataset;
            foreach (var d in ds)
            {
                var vals = d.Value as IList<RDFDataset.Quad>;
                foreach (var val in vals)
                {
                    Console.WriteLine( val.GetObject() + " " + val.GetPredicate() + " " + val.GetSubject() );
                }

            }
            
            //BreadthFirstTraversal(token);

            //Assert.IsNotNull(token);
        }

        private void BreadthFirstTraversal( JToken node )
        {
            if ( node == null )
            {
                return;
            }

            Queue.Enqueue( node.Next );

            Queue.Enqueue( node.First );

            Console.WriteLine( node[ "@context" ] );
            Console.WriteLine( node[ "@id" ] );
            Console.WriteLine( node[ "@type" ] );

            if ( Queue.Count != 0 )
            {
                BreadthFirstTraversal( Queue.Dequeue( ) );
            }
            if ( Queue.Count != 0 )
            {
                BreadthFirstTraversal( Queue.Dequeue( ) );
            }
        }
    }
}
