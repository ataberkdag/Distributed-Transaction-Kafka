using Core.Application.Services;
using Core.Domain.Base;
using Dapper;
using Newtonsoft.Json;

namespace OutboxWorkerService
{
    public class StockOutboxWorker : BackgroundService
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IEventProducer _producer;

        public StockOutboxWorker(IEnumerable<IDbConnectionFactory> dbConnectionFactories,
            IEventProducer producer)
        {
            _dbConnectionFactory = dbConnectionFactories.First(x => x.GetDbName() == "StockDb");
            _producer = producer;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

            while(await timer.WaitForNextTickAsync(stoppingToken))
            {
                string sql = $@"         SELECT
                                          ""Id"",
                                          ""Type"",
                                          ""Data"",
                                          ""TopicName"",
                                          ""CreatedOn""
                                     FROM public.""OutboxMessage""
                                     ORDER BY ""Id""
                                     LIMIT 100 FOR UPDATE SKIP LOCKED; 
                                ";

                var connection = _dbConnectionFactory.GetOpenConnection();

                var messages = await connection.QueryAsync<OutboxMessage>(sql);

                var listOfIds = new List<long>();
                foreach (var outboxMessage in messages)
                {
                    try
                    {
                        await this._producer.ProduceEvent(outboxMessage.TopicName, outboxMessage.Data);

                        listOfIds.Add(outboxMessage.Id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error");
                    }
                }

                if (listOfIds.Count > 0)
                {
                    var transaction = connection.BeginTransaction();
                    await connection.ExecuteAsync($@"DELETE FROM public.""OutboxMessage"" WHERE ""Id"" IN ('{string.Join("','", listOfIds)}')");
                    transaction.Commit();
                }
            }
        }
    }
}
