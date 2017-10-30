using System;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Core;
using Couchbase.N1QL;
using Microsoft.Extensions.Options;
using Restful.Configuration;

namespace Restful.Models
{
    public class RecordModel
    {
        readonly IBucket _bucket;
        readonly string _bucketName;

        public RecordModel(Cluster cluster, IOptions<CouchbaseSettings> settings)
        {
            _bucket = cluster.OpenBucket(settings.Value.Bucket);
            _bucketName = settings.Value.Bucket;
        }

        public async Task<dynamic> GetByDocumentId(Guid documentId)
        {
            var n1ql = "SELECT firstname, lastname, email " +
                       "FROM `" + _bucketName + "` AS users " +
                       "WHERE META(users).id = $1";
            var query = QueryRequest.Create(n1ql);
            query.AddPositionalParameter(documentId);

            var r = await _bucket.QueryAsync<dynamic>(query);
            return r.Rows.FirstOrDefault();

            // getting a single document by ID is not an efficient use of N1QL/Couchbase
            // so the above is just an instructional example
            // it would be better to do this in practice:
            // return _bucket.Get<dynamic>(documentId.ToString());
        }

        public async Task<dynamic> GetAll()
        {
            var n1ql = "SELECT META(users).id, firstname, lastname, email " +
                        "FROM `" + _bucketName + "` AS users ";
            var query = QueryRequest.Create(n1ql);
            query.ScanConsistency(ScanConsistency.RequestPlus);

            var r = await _bucket.QueryAsync<dynamic>(query);
            return r.Rows;
        }

        public async Task<dynamic> Query(string n1ql)
        {
            var queryToRun = QueryRequest.Create(n1ql);
            var results = await _bucket.QueryAsync<dynamic>(queryToRun);
            return results.Rows;
        }

        public Task<IDocumentResult<dynamic>> Save(Person data)
        {
            var document = new Document<dynamic> {
                Id = data.Document_Id?.ToString() ?? Guid.NewGuid().ToString(),
                Content = new {
                    firstname = data.FirstName,
                    lastname = data.LastName,
                    email = data.Email,
                    type = "User"
                }
            };

            return _bucket.UpsertAsync(document);
        }

        public Task<IOperationResult> Delete(Guid documentId) =>
            _bucket.RemoveAsync(documentId.ToString());
    }
}
