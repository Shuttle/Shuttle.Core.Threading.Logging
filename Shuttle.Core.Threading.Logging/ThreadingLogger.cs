using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Threading.Logging;

public class ThreadingLogger : IHostedService
{
    private readonly ThreadingOptions _threadingOptions;
    private readonly ILogger<ThreadingLogger> _logger;

    public ThreadingLogger(ILogger<ThreadingLogger> logger, IOptions<ThreadingOptions> threadingOptions)
    {
        _logger = Guard.AgainstNull(logger);
        _threadingOptions = Guard.AgainstNull(Guard.AgainstNull(threadingOptions).Value);

        _threadingOptions.ProcessorException += ProcessorException;
        _threadingOptions.ProcessorExecuting += ProcessorExecuting;
        _threadingOptions.ProcessorExecuted += ProcessorExecuted ;
        _threadingOptions.ProcessorThreadActive += ProcessorThreadActive;
        _threadingOptions.ProcessorThreadStarting += ProcessorThreadStarting;
        _threadingOptions.ProcessorThreadStopped += ProcessorThreadStopped;
        _threadingOptions.ProcessorThreadStopping += ProcessorThreadStopping;
        _threadingOptions.ProcessorThreadOperationCanceled += ProcessorThreadOperationCanceled;
    }

    private Task ProcessorExecuting(ProcessorExecutingEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace(@"[ProcessorExecuting] : service key = '{EventArgsServiceKey}' / processor = {ProcessorFullName} / managed thread id = {ManagedThreadId}", eventArgs.ServiceKey, GetProcessorFullName(eventArgs.Processor), eventArgs.ManagedThreadId);

        return Task.CompletedTask;
    }

    private Task ProcessorExecuted(ProcessorExecutedEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace(@"[ProcessorExecuted] : service key = '{EventArgsServiceKey}' / processor = {ProcessorFullName} / managed thread id = {ManagedThreadId}", eventArgs.ServiceKey, GetProcessorFullName(eventArgs.Processor), eventArgs.ManagedThreadId);

        return Task.CompletedTask;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _threadingOptions.ProcessorException -= ProcessorException;
        _threadingOptions.ProcessorExecuting -= ProcessorExecuting;
        _threadingOptions.ProcessorExecuted -= ProcessorExecuted;
        _threadingOptions.ProcessorThreadActive -= ProcessorThreadActive;
        _threadingOptions.ProcessorThreadStarting -= ProcessorThreadStarting;
        _threadingOptions.ProcessorThreadStopped -= ProcessorThreadStopped;
        _threadingOptions.ProcessorThreadStopping -= ProcessorThreadStopping;
        _threadingOptions.ProcessorThreadOperationCanceled -= ProcessorThreadOperationCanceled;

        return Task.CompletedTask;
    }

    private static string GetProcessorFullName(IProcessor processor)
    {
        return processor.GetType().FullName ?? processor.GetType().Name;
    }

    private Task ProcessorException(ProcessorThreadExceptionEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[ProcessorException] : service key = '{ServiceKey}' / managed thread id = {ManagedThreadId} / exception = '{Exception}'", eventArgs.ProcessorThread.ServiceKey, eventArgs.ManagedThreadId, eventArgs.Exception);

        return Task.CompletedTask;
    }

    private Task ProcessorThreadActive(ProcessorThreadEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[ProcessorThreadActive] : service key = '{ServiceKey}' / managed thread id = {ManagedThreadId}", eventArgs.ProcessorThread.ServiceKey, eventArgs.ManagedThreadId);

        return Task.CompletedTask;
    }

    private Task ProcessorThreadOperationCanceled(ProcessorThreadEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[ProcessorThreadOperationCanceled] : service key = '{ServiceKey}' / managed thread id = {ManagedThreadId}", eventArgs.ProcessorThread.ServiceKey, eventArgs.ManagedThreadId);

        return Task.CompletedTask;
    }

    private Task ProcessorThreadStarting(ProcessorThreadEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[ProcessorThreadStarting] : service key = '{ServiceKey}' / managed thread id = {ManagedThreadId}", eventArgs.ProcessorThread.ServiceKey, eventArgs.ManagedThreadId);

        return Task.CompletedTask;
    }

    private Task ProcessorThreadStopped(ProcessorThreadEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[ProcessorThreadStopped] : service key = '{ServiceKey}' / managed thread id = {ManagedThreadId}", eventArgs.ProcessorThread.ServiceKey, eventArgs.ManagedThreadId);

        return Task.CompletedTask;
    }

    private Task ProcessorThreadStopping(ProcessorThreadEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[ProcessorThreadStopping] : service key = '{ServiceKey}' / managed thread id = {ManagedThreadId}", eventArgs.ProcessorThread.ServiceKey, eventArgs.ManagedThreadId);

        return Task.CompletedTask;
    }
}