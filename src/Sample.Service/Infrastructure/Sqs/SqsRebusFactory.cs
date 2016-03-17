using Amazon;
using Amazon.SQS;
using Autofac;
using Rebus.AmazonSQS.Config;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Retry.Simple;
using Rebus.Routing.TypeBased;
using Sample.MessageContracts.Events;
using Sample.Service.Infrastructure.Rebus.Autofac;
using Sample.Service.Infrastructure.Rebus.Serilog;
using Serilog;

namespace Sample.Service.Infrastructure.Sqs
{
    public class SqsRebusFactory
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly ILogger _logger;
        private readonly string _inputQueueAddress;
        private readonly string _errorQueueAddress;

        public SqsRebusFactory(
            ILifetimeScope lifetimeScope, 
            ILogger logger)
        {
            _lifetimeScope = lifetimeScope;
            _logger = logger;

            _inputQueueAddress = MessageContracts.Constants.InputQueueAddress;
            _errorQueueAddress = $"{_inputQueueAddress}_Error";
        }

        public IBus Create()
        {
            var sqsConfig = new AmazonSQSConfig
            {
                RegionEndpoint = RegionEndpoint.APSoutheast2
            };

            var bus = Configure.With(new AutofacContainerAdapter(_lifetimeScope))
                .Logging(l => l.Serilog(_logger))
                .Transport(
                    t =>
                        t.UseAmazonSqs("{AccessKey}", "{SecretKey}", sqsConfig,
                            _inputQueueAddress))
                .Routing(r => r.TypeBased().MapAssemblyOf<PoloEvent>(_inputQueueAddress)) 
                .Options(o =>
                {
                    o.SetMaxParallelism(1);
                    o.SetNumberOfWorkers(1);
                    o.SimpleRetryStrategy(errorQueueAddress: _errorQueueAddress);
                    //o.EnableUnitOfWork(Initiate, Commmit, Rollback);
                })
                .Start();

            bus.Subscribe<PoloEvent>();

            return bus;
        }

        //private Task Rollback(IMessageContext messageContext, IUnitOfWork uow)
        //{
        //    return uow.Abandon();
        //}

        //private Task Commmit(IMessageContext messageContext, IUnitOfWork uow)
        //{
        //    return uow.Complete();
        //}

        //private Task<IUnitOfWork> Initiate(IMessageContext messageContext)
        //{
        //    var scope = messageContext.TransactionContext.GetOrAdd("current-autofac-lifetime-scope", () => _lifetimeScope.BeginLifetimeScope());

        //    return Task.FromResult(scope.Resolve<IUnitOfWork>());
        //}
    }
}