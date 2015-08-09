using System;
using System.IO;
using System.Runtime.InteropServices;
using JsonLdTypeMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchemaSpider.Tests
{
    [TestClass]
    public class JsonLdTests
    {
        [TestMethod]
        public void ShouldReturnJTokenWhenFileIsPresent( )
        {
            var testFile = "schemaorg_ex.jsonld";
            var file = new JsonLdFile(Path.Combine(Environment.CurrentDirectory, "TestFiles", testFile));

            var token = file.Read();

            

            Assert.IsNotNull(token);
        }
    }
}
