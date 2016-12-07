using System;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Core;
using Couchbase.N1QL;

namespace Restfulangularcore.Models
{
    public class RecordModel
    {
        readonly IBucket _bucket;

        public RecordModel()
        {
            _bucket = ClusterHelper.GetBucket("default"); //ConfigurationManager.AppSettings["CouchbaseBucket"]);
        }


        public async Task<dynamic> GetByDocumentId(Guid documentId)
        {
            var n1ql = "SELECT firstname, lastname, email " +
                       "FROM `default` AS users " + //ConfigurationManager.AppSettings["CouchbaseBucket"] + "` AS users " +
                       "WHERE META(users).id = $1";
            var query = QueryRequest.Create(n1ql);
            query.AddPositionalParameter(documentId);

            var r = await _bucket.QueryAsync<dynamic>(query);
            return r.Rows.FirstOrDefault();
        }

        public async Task<dynamic> GetAll()
        {
            var n1ql = "SELECT META(users).id, firstname, lastname, email " +
                        "FROM `default` AS users "; //+ ConfigurationManager.AppSettings["CouchbaseBucket"] + "` AS users";
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

/*
        public async Task<dynamic> FindAllRoutes(string from, string to)
        {
            var statement = @"SELECT faa AS fromAirport, geo 
                              FROM `" + ConfigurationManager.AppSettings["CouchbaseBucket"] + @"` r
                              WHERE airportname = $1
                              UNION SELECT faa AS toAirport, geo
                              FROM `" + ConfigurationManager.AppSettings["CouchbaseBucket"] + @"` r
                              WHERE airportname = $2";
            var queryToRun = QueryRequest.Create(statement);
            queryToRun.AddPositionalParameter(from);
            queryToRun.AddPositionalParameter(to);
            var results = await _bucket.QueryAsync<dynamic>(queryToRun);
            return results.Rows;
        }

        public async Task<dynamic> FindAllRoutesOnDay(string queryFrom, string queryTo, int leave)
        {
            var statement = @"SELECT r.id, a.name, s.flight, s.utc, r.sourceairport, r.destinationairport, r.equipment
                              FROM `" + ConfigurationManager.AppSettings["CouchbaseBucket"] + @"` r
                              UNNEST r.schedule s
                              JOIN `" + ConfigurationManager.AppSettings["CouchbaseBucket"] + @"` a ON KEYS r.airlineid
                              WHERE r.sourceairport = $1
                              AND r.destinationairport = $2
                              AND s.day = $3
                              ORDER BY a.name";
            var queryToRun = QueryRequest.Create(statement);
            queryToRun.AddPositionalParameter(queryFrom);
            queryToRun.AddPositionalParameter(queryTo);
            queryToRun.AddPositionalParameter(leave);
            var results = await _bucket.QueryAsync<dynamic>(queryToRun);
            return results.Rows;
        }
        */
    }
}
