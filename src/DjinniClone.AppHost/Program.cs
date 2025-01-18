using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var identityPostgres = builder.AddPostgres("IdentityPostgres")
    .WithImage("postgres", "15.8")
    .WithHttpEndpoint(targetPort: 6000)
    .WithDataVolume()
    .WithPgAdmin();

var chatPostgres = builder.AddPostgres("ChatPostgres")
    .WithImage("postgres", "15.8")
    .WithHttpEndpoint(targetPort: 6001)
    .WithDataVolume()
    .WithPgAdmin();

var profilePostgres = builder.AddPostgres("ProfilePostgres")
    .WithImage("postgres", "15.8")
    .WithHttpEndpoint(targetPort: 6002)
    .WithDataVolume()
    .WithPgAdmin();

var vacanciesPostgres = builder.AddPostgres("VacanciesPostgres")
    .WithImage("postgres", "15.8")
    .WithHttpEndpoint(targetPort: 6003)
    .WithDataVolume()
    .WithPgAdmin();

var rabbitMq = builder.AddRabbitMQ("RabbitMq")
    .WithManagementPlugin();

var redis = builder.AddRedis("Redis");

var ollama = builder.AddOllama(name: "ollama", port: null)
    .WithOpenWebUI()
    .WithDataVolume()
    .PublishAsContainer()
    .AddModel("phi3.5");

// var apiGateway = builder.AddProject<Projects.ApiGateway>("apigateway")
//     .WithExternalHttpEndpoints();

var chatService = builder.AddProject<ChatService>("chatservice")
    .WithReference(chatPostgres)
    .WithReference(redis)
    .WithReference(rabbitMq)
    .WaitFor(chatPostgres)
    .WaitFor(redis)
    .WaitFor(rabbitMq);

var identityService = builder.AddProject<IdentityService>("identityservice")
    .WithReference(identityPostgres)
    .WithReference(redis)
    .WithReference(rabbitMq)
    .WaitFor(identityPostgres)
    .WaitFor(redis)
    .WaitFor(rabbitMq);

var profileService = builder.AddProject<ProfilesService>("profilesservice")
    .WithReference(profilePostgres)
    .WithReference(redis)
    // .WithReference(ollama)
    .WithReference(rabbitMq)
    .WaitFor(profilePostgres)
    .WaitFor(redis)
    .WaitFor(rabbitMq);

var vacanciesService = builder.AddProject<VacanciesService>("vacanciesservice")
    .WithReference(vacanciesPostgres)
    .WithReference(redis)
    .WithReference(rabbitMq)
    .WaitFor(vacanciesPostgres)
    .WaitFor(redis)
    .WaitFor(rabbitMq);

var isHttps = builder.Configuration["DOTNET_LAUNCH_PROFILE"] == "https";
var ingressPort = int.TryParse(builder.Configuration["Ingress:Port"], out var port) ? port : (int?)null;


builder.AddYarp("ingress")
    .WithEndpoint(scheme: "http", port: 5000)
    .WithReference(chatService)
    .WithReference(vacanciesService)
    .WithReference(profileService)
    .WithReference(identityService)
    .LoadFromConfiguration("ReverseProxy");


builder.Build().Run();