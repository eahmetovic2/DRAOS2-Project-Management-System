using MongoDB.Bson;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Common.Helpers
{
    public class DocumentWrapper
    {
        public string Collection { get; set; }

        public BsonDocument Document { get; set; }
    }


    public static class DBLogHelper
    {

        static ConcurrentQueue<DocumentWrapper> _docs;
        static object syncObject = new object();

        public static ConcurrentQueue<DocumentWrapper> Docs
        {
            get
            {
                if (_docs == null)
                {
                    lock (syncObject)
                    {
                        if (_docs == null)
                        {
                            _docs = new ConcurrentQueue<DocumentWrapper>();
                        }
                    }
                }
                return _docs;
            }
        }
    }
}
