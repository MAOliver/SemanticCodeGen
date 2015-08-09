using System.IO;
using JsonLD.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SchemaSpider.Core;

namespace JsonLdTypeMapper
{
    public class JsonLdFile
    {
        private readonly string _filename;

        public JsonLdFile(string jsonLdFilename)
        {
            _filename = jsonLdFilename;
        }

        public JToken Read()
        {
            using (StreamReader sr = new StreamReader(_filename))
            {
                return JSONUtils.FromReader(sr);
            }
        }

        public CSharpSourceBuilder ConvertToCSharp(JToken token)
        {
            var sourceBuilder = CSharpSourceBuilder.New();
            return sourceBuilder;
        }

        private JToken GetJson( JToken j )
        {
            try
            {
                if ( j.Type == JTokenType.Null )
                    return null;
                using ( Stream manifestStream = File.OpenRead( "W3C\\" + ( string ) j ) )
                using ( TextReader reader = new StreamReader( manifestStream ) )
                using ( JsonReader jreader = new JsonTextReader( reader ) )
                {
                    return JToken.ReadFrom( jreader );
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
