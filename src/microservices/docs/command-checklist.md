# Command Checklist
This is a quick check list to run through before spinning up the app.

## API Gateway

### Command Class

- [ x] Create command class implementing `ICommand`
- [ x] Readonly properties
- [ x] `[JsonConstructor]` attribute to constructor.
- [x ] `[MicroService]` attribute applied with intended service name.

### Controller

- [ x] Create action method which using the command as a parameter
- [ x] `[FromBody]` attribute used for the parameter passed.
- [ x] `PublishCommand<T>` used from the `GatewayControllerBase` class.

## Handling Service

- [ x] Copied class from gateway with the same properties and removal of `[MicroService]` attribute.
- [ x] Used the `SubscribeCommand<T>` method in the startup class.
- [ x] Created a command handler implementing `ICommandHandler<T>`
- [ x] Added DI mapping for the Command Handler to `ServiceRegistry` class.
- [ x] Created a success event implementing `IEvent`
- [ x] Created a rejected event implementing `IRejectedEvent`
- [ x] Publish both events in failed and success states of the handler.
- [ x] Copied both classes to the operations service and added the `[MicroService]` attribute. Ensuring `[JsonContructor]` attribute is applied.
- [ x] Unit test written for the handler class testing all execution paths.

