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

        _threadingOptions.ProcessorException += OnProcessorException;
        _threadingOptions.ProcessorExecuting += OnProcessorExecuting;
        _threadingOptions.ProcessorThreadActive += OnProcessorThreadActive;
        _threadingOptions.ProcessorThreadStarting += OnProcessorThreadStarting;
        _threadingOptions.ProcessorThreadStopped += OnProcessorThreadStopped;
        _threadingOptions.ProcessorThreadStopping += OnProcessorThreadStopping;
        _threadingOptions.ProcessorThreadOperationCanceled += OnProcessorThreadOperationCanceled;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _threadingOptions.ProcessorException -= OnProcessorException;
        _threadingOptions.ProcessorExecuting -= OnProcessorExecuting;
        _threadingOptions.ProcessorThreadActive -= OnProcessorThreadActive;
        _threadingOptions.ProcessorThreadStarting -= OnProcessorThreadStarting;
        _threadingOptions.ProcessorThreadStopped -= OnProcessorThreadStopped;
        _threadingOptions.ProcessorThreadStopping -= OnProcessorThreadStopping;
        _threadingOptions.ProcessorThreadOperationCanceled -= OnProcessorThreadOperationCanceled;

        return Task.CompletedTask;
    }

    private static string GetProcessorFullName(ProcessorThread processorThread)
    {
        return processorThread.GetType().FullName ?? processorThread.GetType().Name;
    }

    private Task OnProcessorException(ProcessorThreadExceptionEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace($@"[ProcessorException] : name = '{eventArgs.ProcessorThread.Name}' / processor = {GetProcessorFullName(eventArgs.ProcessorThread)} / managed thread id = {eventArgs.ManagedThreadId} / exception = '{eventArgs.Exception}'");

        return Task.CompletedTask;
    }

    private Task OnProcessorExecuting(ProcessorThreadEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace($@"[ProcessorExecuting] : name = '{eventArgs.ProcessorThread.Name}' / processor = {GetProcessorFullName(eventArgs.ProcessorThread)} / managed thread id = {eventArgs.ManagedThreadId}");

        return Task.CompletedTask;
    }

    private Task OnProcessorThreadActive(ProcessorThreadEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace($@"[ProcessorThreadActive] : name = '{eventArgs.ProcessorThread.Name}' / processor = {GetProcessorFullName(eventArgs.ProcessorThread)} / managed thread id = {eventArgs.ManagedThreadId}");

        return Task.CompletedTask;
    }

    private Task OnProcessorThreadOperationCanceled(ProcessorThreadEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace($@"[ProcessorThreadOperationCanceled] : name = '{eventArgs.ProcessorThread.Name}' / processor = {GetProcessorFullName(eventArgs.ProcessorThread)} / managed thread id = {eventArgs.ManagedThreadId}");

        return Task.CompletedTask;
    }

    private Task OnProcessorThreadStarting(ProcessorThreadEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace($@"[ProcessorThreadStarting] : name = '{eventArgs.ProcessorThread.Name}' / processor = {GetProcessorFullName(eventArgs.ProcessorThread)} / managed thread id = {eventArgs.ManagedThreadId}");

        return Task.CompletedTask;
    }

    private Task OnProcessorThreadStopped(ProcessorThreadEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace($@"[ProcessorThreadStopped] : name = '{eventArgs.ProcessorThread.Name}' / processor = {GetProcessorFullName(eventArgs.ProcessorThread)} / managed thread id = {eventArgs.ManagedThreadId}");

        return Task.CompletedTask;
    }

    private Task OnProcessorThreadStopping(ProcessorThreadEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace($@"[ProcessorThreadStopping] : name = '{eventArgs.ProcessorThread.Name}' / processor = {GetProcessorFullName(eventArgs.ProcessorThread)} / managed thread id = {eventArgs.ManagedThreadId}");

        return Task.CompletedTask;
    }
}