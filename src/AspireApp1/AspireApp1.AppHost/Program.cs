var builder = DistributedApplication.CreateBuilder(args);

var rabbitMq = builder.AddRabbitMQ("rabbitmq");
var redis = builder.AddRedis("Redis");

var identityPostgres = builder.AddPostgres(name: "PostgresIdentity", password:"postgres", port: 50031).AddDatabase("IdentityDb");
var profilePostgres = builder.AddPostgres(name: "PostgresProfile",password:"postgres", port:50034).AddDatabase("ProfileDb");
var vacanciesPostgres = builder.AddPostgres(name: "PostgresVacancies", password:"postgres", port:50037).AddDatabase("VacanciesDb");
var chatPostgres = builder.AddPostgres(name: "PostgresChat", password:"postgres", port:50040).AddDatabase("ChatDb");

var chatService = builder.AddProject<Projects.ChatService>("ChatService")
    .WithReference(chatPostgres)
    .WithReference(rabbitMq)
    .WithReference(redis);
var identityService = builder.AddProject<Projects.IdentityService>("IdentityService")
    .WithReference(identityPostgres)
    .WithReference(rabbitMq)
    .WithReference(redis);
var vacanciesService = builder.AddProject<Projects.VacanciesService>("VacanciesService")
    .WithReference(vacanciesPostgres)
    .WithReference(rabbitMq)
    .WithReference(redis);
var profileService = builder.AddProject<Projects.ProfilesService>("ProfileService")
    .WithReference(profilePostgres)
    .WithReference(rabbitMq)
    .WithReference(redis);


builder.AddNpmApp("frontend", "../../client", "dev")
    .WithReference(chatService)
    .WithReference(identityService)
    .WithReference(vacanciesService)
    .WithReference(profileService);

builder.AddProject<Projects.ApiGateway>("ApiGateway")
    .WithReference(chatService)
    .WithReference(identityService)
    .WithReference(vacanciesService)
    .WithReference(profileService);


builder.Build().Run();
