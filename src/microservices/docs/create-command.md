# Steps to create a write in VMS
This will walk through the process of creating a command that starts at the api gateway and works its way through to the intended service and then how this can be picked up by the operations service to provide feedback in the completion of the operation.

## Gatway Setup

### Create Command (API.Gateway)
In order to create a command create a class such as the follwoing noting the marker interface `ICommand` used for generic contraints in the helper methods for dispatching. In addition note the readonly properties and the `[JsonConstructor]` attribute applied to allow for deserilisation of the object from the web. Their is also a attribute applied to the command in the Api.Gateway which marks the service is which the command is intended for this is the `[MicroService("-name-")]` attribute. An example command can be seen below:

```csharp
[MicroService(ServiceNames.Test)]
public class TestCommand : ICommand
{
    public bool IsPass { get; }

    [JsonConstructor]
    public TestCommand(bool isPass)
    {
        IsPass = isPass;
    }
}
```

### Open endpoint for command
In order to take the data from a client a c# controller is used this uses the extension of the `ControllerBase` class `GatewayControllerBase` which adds a few simple methods to allow for consitensy.

The main key points to note about the class below are:
* `GatewayControllerBase` Implementation.
* `[FromBody` attribute in the command paramter to the controller method
* `[Route("[Route("gateway/api/-name-/")]")]` where -name- is replaced with the controller name.
* `[HttpPost("-method-name-)]` where the method name is applied to form the full route.
* Use of `PublishCommand<T>` helper method from the base class.
* `[Authorize]` attribute to mark access restrictions `[AllowAnnoynmous]` should be used to allow public access.

An example controller class can be found below:
```csharp
[Route("gateway/api/test/")]
public class TestController : GatewayControllerBase
{
    public TestController(IDispatcher dispatcher) : base(dispatcher) { }

    [Authorize(Roles = Roles.SystemAdmin)]
    [HttpPost("pass")]
    public IActionResult Pass([FromBody]TestCommand test)
    {
        return PublishCommand(test);
    }
}
```

## Service Side (Services.-ServiceName-)
This will walk through the steps to handle a command on the service side once it has been placed onto the message bus.

### Subscribing to a command
In order to subscribe to a command the class created in the Api.Gateway can be copied with one suttle change which is the removal of the `[MicroService]` attribute. An example of this can be seen below:

```csharp
public class TestCommand : ICommand
{
    public bool IsPass { get; }

    [JsonConstructor]
    public TestCommand(bool isPass)
    {
        IsPass = isPass;
    }
}
```

The next step is to use the helper function provided as part of the `IServiceBusManager` class provided either through DI as a singleton or on the `ApplicationBuilder` extension `UseServiceBus()` method. An example using the latter can be seen below:

```csharp
app.UseServiceBus(ServiceNames.Test, true)
                .SubscribeCommand<TestCommand>();
```

This then relys on the implementation of a handler class which is explained in the next section.

### Creating Command Handler `ICommandHandler<T>`

In order for the command to sucesfully be subscribed a command handler must be written. A class can be created using the interface `ICommandHandler<T>` where the generic class is the command in which the handler should handle. An example of the implementation of a command handler is below:
```csharp
public class TestCommandHandler : ICommandHandler<TestCommand>
{
    private readonly IServiceBusMessagePublisher _serviceBusMessagePublisher;

    public TestCommandHandler(IServiceBusMessagePublisher serviceBusMessagePublisher)
    {
        _serviceBusMessagePublisher = serviceBusMessagePublisher;
    }

    public Task HandleAsync(TestCommand message, IRequestInfo requestInfo)
    {
        if (message.IsPass)
        {
            _serviceBusMessagePublisher.PublishEvent(new TestCommandPassed("This is a happy message"), requestInfo);
        }
        else
        {
            _serviceBusMessagePublisher.PublishEvent(new TestCommandRejected("failed", "The parameter passed was false."), requestInfo);
        }

        return Task.CompletedTask;
    }
}
```
This example also shows how events can be published from within a command handler this is expanded on in the LINK: events.md. These can then also be handled by the operations service details on this can be found in the LINK: operations.md

