using Emertec.Application.Extension;
using Emertec.Infrastructure.Extension;
using MicroService_Template.Application.DTO;
using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Application.Services;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Setting;
using MicroService_Template.Infrastructure.Repository;
using MicroService_Template.Job;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Listener;


{

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddQuartz(q =>
    {
        var mp3ToRsaJobKey = new JobKey("MP3ToRSAWorker");
        var rsaToJsonJobKey = new JobKey("RSAToJsonWorker");
        var jsonToDbJobKey = new JobKey("JsonToDbWorker");

        // Register jobs
        q.AddJob<MP3ToRSAWorker>(opts => opts.WithIdentity(mp3ToRsaJobKey));
        q.AddJob<RSAToJsonWorker>(opts => opts.WithIdentity(rsaToJsonJobKey));
        q.AddJob<JsonToDbWorker>(opts => opts.WithIdentity(jsonToDbJobKey));

        // Get individual cron expressions
        var mp3ToRsaCron = builder.Configuration["Quartz:MP3ToRSAJob"];
        var rsaToJsonCron = builder.Configuration["Quartz:RSAToJsonJob"];
        var jsonToDbCron = builder.Configuration["Quartz:JsonToDbJob"];

        // Add triggers for each job
        q.AddTrigger(opts => opts
            .ForJob(mp3ToRsaJobKey)
            .WithIdentity("TriggerMP3ToRSA")
            .WithCronSchedule(mp3ToRsaCron, cron => cron.WithMisfireHandlingInstructionDoNothing()));

        q.AddTrigger(opts => opts
            .ForJob(rsaToJsonJobKey)
            .WithIdentity("TriggerRSAToJson")
            .WithCronSchedule(rsaToJsonCron, cron => cron.WithMisfireHandlingInstructionDoNothing()));

        q.AddTrigger(opts => opts
            .ForJob(jsonToDbJobKey)
            .WithIdentity("TriggerJsonToDb")
            .WithCronSchedule(jsonToDbCron, cron => cron.WithMisfireHandlingInstructionDoNothing()));
    });

    // Hosted service to run Quartz
    builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);



    //REPOSITORY INJECTION

    builder.Services.AddScoped<IModelDimJsonRepository, ModelDimJsonRepository>();
    builder.Services.AddScoped<IModelDimCompanyRepository, ModelDimCompanyRepository>();
    builder.Services.AddScoped<IModelDimCampaignRepository, ModelDimCampaignRepository>();
    builder.Services.AddScoped<IModelDimTextFullRepository, ModelDimTextFullRepository>();
    builder.Services.AddScoped<IModelDimAgentRepository, ModelDimAgentRepository>();
    builder.Services.AddScoped<IModelDimTextSentenceRepository, IModelDimTextWordRepository>();
    builder.Services.AddScoped<IModelDimTextWordRepositorycs, ModelDimTextWordRepositorycs>();


    //SERVICE INJECTION 
    builder.Services.AddScoped<IAudioFileService, AudioFileService>();
    builder.Services.AddScoped<IConvertRsaToJsonService, ConvertRsaToJsonService>();
    builder.Services.AddScoped<ICryptoService, CryptoService>();
    builder.Services.AddScoped<IConvertJsonToDbService, ConvertJsonToDbService>();


    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddInfrastrucureService();
    builder.Services.AddApplicationService();
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Emertec")));
    builder.Services.Configure<AudioPaths>(builder.Configuration.GetSection("AudioEncryptionPaths"));
    builder.Services.Configure<MP3Settings>(builder.Configuration.GetSection("MP3Settings"));
    builder.Services.Configure<DecryptRequest>(builder.Configuration.GetSection("DecryptRequest"));
    builder.Services.Configure<KeyGenerationResponse>(builder.Configuration.GetSection("Crypto"));
    builder.Services.Configure<JsonToDB>(builder.Configuration.GetSection("JsonToDB"));



    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UseStaticFiles();

    app.MapControllers();

    

    app.Run();
}


