// See https://aka.ms/new-console-template for more information

using BaddEcon.Core;
using BaddEcon.Core.Infrastructure.Storage;
using BaddEcon.Core.Services.Lookups;
using Bass.Shared.Infrastructure.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;


var builder = Host.CreateApplicationBuilder(args);
//builder.ConfigureServices((context, svcs) => { ConfigureServices(context.Configuration, svcs); })

builder.Services
   .Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"))
   .AddSingleton<IMongoDbSettings>(serviceProvider =>
      serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value)
   .AddSingleton<IMongoContext, MongoContext>()
   
   .AddScoped<IRawResourceTypeRepo, RawResourceTypeRepo>()
   .AddScoped<IRefinedResourceTypeRepo, RefinedResourceTypeRepo>()
   
   .AddScoped<IRawResourceTypeLookup, RawResourceTypeLookup>()
   
   .AddHostedService<ExampleHostedService>();
IHost host = builder.Build();
host.Run();

var mongoSettings = new MongoDbSettings
{
   ConnectionString = "mongodb+srv://brentacs:OKibx3ElcSWTl2Z5@blueharvestserverlessin.vkgora5.mongodb.net/admin?retryWrites=true&replicaSet=atlas-xh08cp-shard-0&readPreference=primary&srvServiceName=mongodb&connectTimeoutMS=10000&authSource=admin&authMechanism=SCRAM-SHA-1",
   DatabaseName = "BaddEcon"
};

var mongoContext = new MongoContext(mongoSettings);
//
// var coll = mongoContext.Db.GetCollection<Commodity>("Commodities");
// var commodity1 = new Commodity
// {
//    // _id = "Commodity-2",
//    Name = "Commodity-3",
//    ShortDescription = "A short description, again",
//    WeightType = WeightType.MetricTon
// };
// coll.InsertOne(commodity1);

// Logger.

// var repo = new RawResourceTypeRepo(mongoContext, );

Console.WriteLine("Done!");
