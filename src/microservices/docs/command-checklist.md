# Command Checklist
This is a quick check list to run through before spinning up the app.

## API Gateway

### Command Class

- [ ] Create command class implementing `ICommand`
- [ ] Readonly properties
- [ ] `[JsonConstructor]` attribute to constructor.
- [ ] `[MicroService]` attribute applied with intended service name.

### Controller

- [ ] Create action method which using the command as a parameter
- [ ] `[FromBody]` attribute used for the parameter passed.
- [ ] `PublishCommand<T>` used from the `GatewayControllerBase` class.

## Handling Service

- [ ] Copied class from gateway with the same properties and removal of `[MicroService]` attribute.
- [ ] Used the `SubscribeCommand<T>` method in the startup class.
- [ ] Created a command handler implementing `ICommandHandler<T>`
- [ ] Added DI mapping for the Command Handler to `ServiceRegistry` class.
- [ ] Created a success event implementing `IEvent`
- [ ] Created a rejected event implementing `IRejectedEvent`
- [ ] Publish both events in failed and success states of the handler.
- [ ] Copied both classes to the operations service and added the `[MicroService]` attribute. Ensuring `[JsonContructor]` attribute is applied.
- [ ] Unit test written for the handler class testing all execution paths.

