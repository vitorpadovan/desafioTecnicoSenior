using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddProduct_MongoDb
{
    public class ProdutoCol
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("id")]
        public int ProductId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("tock")]
        public int Stock { get; set; }
    }
}
