using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarteiraFinanceira.Entities
{    
    public class MovimentacaoFinanceira : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("valor")]
        public decimal valor { get; set; }

        [BsonElement("tipo")]
        public string? tipo { get; set; }

        [BsonElement("descricaoFinalidade")]
        public string? descricaoFinalidade { get; set; }

        [BsonElement("dataCriacao")]
        public DateTime dataCriacao { get; set; }
    }
}
