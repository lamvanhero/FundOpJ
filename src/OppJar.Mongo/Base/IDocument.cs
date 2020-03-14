using MongoDB.Bson;
using System;

namespace OppJar.Mongo
{
    public interface IDocument
    {
        ObjectId Id { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
