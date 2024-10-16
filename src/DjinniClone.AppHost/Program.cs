var builder = DistributedApplication.CreateBuilder(args);

var identityPostgres = builder.AddPostgres("IdentityPostgres")
    .WithImage("postgres", "15.8")
    .WithDataVolume()
    .WithPgAdmin();

var chatPostgres = builder.AddPostgres("ChatPostgres")
    .WithImage("postgres", "15.8")
    .WithDataVolume()
    .WithPgAdmin();

var profilePostgres = builder.AddPostgres("ProfilePostgres")
    .WithImage("postgres", "15.8")
    .WithDataVolume()
    .WithPgAdmin();

var vacanciesPostgres = builder.AddPostgres("VacanciesPostgres")
    .WithImage("postgres", "15.8")
    .WithDataVolume()
    .WithPgAdmin();

var rabbitMq = builder.AddRabbitMQ("RabbitMq")
    .WithManagementPlugin();

var redis = builder.AddRedis("Redis");


var apiGateway = builder.AddProject<Projects.ApiGateway>("apigateway");

var chatService = builder.AddProject<Projects.ChatService>("chatservice")
    .WithReference(chatPostgres)
    .WithReference(redis)
    .WithReference(rabbitMq);

var identityService = builder.AddProject<Projects.IdentityService>("identityservice")
    .WithReference(identityPostgres)
    .WithReference(redis)
    .WithReference(rabbitMq);

var profileService = builder.AddProject<Projects.ProfilesService>("profilesservice")
    .WithReference(profilePostgres)
    .WithReference(redis)
    .WithReference(rabbitMq);

var vacanciesService = builder.AddProject<Projects.VacanciesService>("vacanciesservice")
    .WithReference(vacanciesPostgres)
    .WithReference(redis)
    .WithReference(rabbitMq);

apiGateway
    .WithReference(chatService)
    .WithReference(identityService)
    .WithReference(profileService)
    .WithReference(vacanciesService);

builder.Build().Run();