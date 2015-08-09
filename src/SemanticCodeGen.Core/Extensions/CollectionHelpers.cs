using System;
using System.Collections;
using System.Linq;

namespace SchemaSpider.Core.Extensions
{
    public static class CollectionHelpers
    {
        public static void AddAllNonBlank<T>( this IList collection, Func<string, T> mappingFunct, params string[ ] stringsToAdd )
        {
            if ( collection == null || stringsToAdd == null )
                return;
            foreach ( var s in stringsToAdd.Where( s => !string.IsNullOrWhiteSpace( s ) ) )
            {
                collection.Add( mappingFunct.Invoke( s ) );
            }
        }

        public static void AddAllNonNull<T1, T2>( this IList collection, Func<T1, T2> mappingFunct, params T1[ ] objsToAdd )
        {
            if ( collection == null || objsToAdd == null )
                return;
            foreach ( var obj in objsToAdd.Where( o => o != null ) )
            {
                collection.Add( mappingFunct.Invoke( obj ) );
            }
        }
    }
}
