# CQRS + MediatR + CodeFirst + FluentValidation

This project is a example of how implement **MediatR** with **.NET Core**, also implements a clear way to do generic sorting, filtering and pagination.

## WebAPI

This layer is just about the DI Container and exposes a REST API **BUT** can be changed to a MVC w/ Razor, an Angular project, a React project and so on, all the validation , logging, performance checker is not coupled to the view layer, here we just register the **Repositories**, **DbContext** and **UnitOfWork**.

Yes, this project has a reference of Persistance, **BUT** only for the DI container. There is no violation of the direction of the dependency, we are **NOT** calling the **DbContext** on the **Controller** or something like that, this is more like [Composition Root](https://blog.ploeh.dk/2011/07/28/CompositionRoot/). As this example is with a WebAPI we add a [Filter](./CQRSExample.WebAPI/Filters/CustomExceptionFilterAttribute.cs) to return the correct status code based on the threw exception.

## Application

Here is the meat of this project, we're using **MediatR** so we separate our **writes** and our **reads** on **Queries** and **Commands**, we can take advantage of the pipeline feature using [RequestLogger](./CQRSExample.Application/Infraestructure/RequestLogger.cs) to log all our queries and commands, in MediatR **everything** is a query or command so we get 100% logging here, with a simple timer we can check the performance of out requests on [RequestPerformanceBehavior](./CQRSExample.Application/Infraestructure/RequestPerformanceBehavior.cs), and we can get automatic validation with `FluentValidation` on [RequestValidationBehavior](./CQRSExample.Application/Infraestructure/RequestValidationBehavior.cs)

Every query and command have a handler, there is where we do the logic: save on db, persist on file or just compute something. The query/command just have the properties we want to use and implements the `IRequest` interface with the return type, the handler implements `IRequestHandler` with a class that implements `IRequest` and the return type, it will implement a Handle method just like this:

```csharp
public class SaveCategoryCommand implements IRequest<int>{
    public string Description { get; set; }
}

public class SaveCategoryCommandHandler implements IRequestHandler<SaveCategoryCommand, int>{.

    public Task<int> Handle(SaveCategoryCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.Description); //Hello world
        return Task.FromResult(1);
    }
}

Mediator.Send(new SaveCategoryCommand { Description = "Hello world" });
```

This layer also have validation on it, we just need to create a class that inherit from `AbstractValidator` with the type we want to validate, the DI container will search all the classes on the assembly and [RequestValidationBehavior](./CQRSExample.Application/Infraestructure/RequestValidationBehavior.cs) will validate automatically when we send the query/command.

```csharp
public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(p => p.Description).MaximumLength(15).NotEmpty();
    }
}
```


We have our DTOs to map from the models, the models are implementation details, we should not expose them, there are on the domain layer for this purpose, we configure the mapping between DTOs and models with a class that inherits from `Profile`

```csharp
public class CategoryProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDTO>();
    }
}
```

Sometimes the shape of the model is different to the shape we want to return, there is when this become a great tool, we can change the shape of our DTO and keep the model the same, we just need one line to do the map.

```csharp
public async Task<KeyValuePairResource> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
{
    var category = mapper.Map<Category>(request); //<-- Here
    await SaveToDB(category);
}
```

We also have a couple of custom exceptions like [DeleteFailureException](./CQRSExample.Application/Infraestructure/Exceptions/DeleteFailureException.cs), [NotFoundException](./CQRSExample.Application/Infraestructure/Exceptions/NotFoundException.cs) and [ValidationException](./CQRSExample.Application/Infraestructure/Exceptions/ValidationException.cs)

## Domain

Here We have our entities model, we dont use data annotations to configure our models to the relational db, that is in the Persistance layer, here we have all our interfaces to communicate the **Persistance** with the **Application** layer, this is the key on **Ioc** (Inversion of control) We not only care to the direction of our classes dependencies, but also de direction of the layer dependency


![Alt text](https://g.gravizo.com/svg?%20@startuml;%20node%20%22Domain%22%20{;%20[IRepositories];%20}%20;%20database%20%22SQL%20Server%22%20{;%20[Data];%20};%20node%20%22Application%22%20{;%20[Queries]%20-%3E%20[IRepositories];%20[Commands]%20-%3E%20[IRepositories];%20}%20;%20node%20%22Persistance%22%20{;%20[DbContext]%20-%3E%20[Data];%20[Repositories]%20-%3E%20[DbContext];%20[Repositories]%20-%3E%20[IRepositories];%20}%20;%20@enduml;)

We use repositories, to **encapsulate** our fat queries and to not expose **IQueryable**, this is the whole idea of the repository pattern. We use this also to  reference the interface, not the implementation.


We also have some models to use as repository parameters, when is necessary, we map the command/query to this model and pass it to the repository.


## Persistance

Here we have the implementation of the Repositories, UnitOfWork, the migrations and the mapping configuration of the models, also a couple extensions methods to do filtering, sorting and paging directly to the DB

