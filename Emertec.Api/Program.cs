using Emertec.Application.Extension;
using Emertec.Infrastructure.Extension;
using MicroService_Template.Application.DTO;
using MicroService_Template.Application.Interface;
using MicroService_Template.Application.Services;
using MicroService_Template.Domain.Setting;
using MicroService_Template.Job;
using Microsoft.EntityFrameworkCore;
using Quartz;


{

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddQuartz(q =>
    {
        // Define the job and trigger
        var jobKey = new JobKey("MP3ToRSAWorker");

        q.AddJob<MP3ToRSAWorker>(opts => opts.WithIdentity(jobKey));
        var cronSchedule = builder.Configuration["Quartz:JobSchedule"];
        q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity("MP3ToRSAWorker")
            .WithCronSchedule(cronSchedule, cronOpts =>
            {
                cronOpts.WithMisfireHandlingInstructionDoNothing(); // Optional
            }));
    });
    builder.Services.AddQuartz(q =>
    {
        // Define the job and trigger
        var jobKey = new JobKey("RSAToJsonWorker");

        q.AddJob<MP3ToRSAWorker>(opts => opts.WithIdentity(jobKey));
        var cronSchedule = builder.Configuration["Quartz:JobSchedule2"];
        q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity("RSAToJsonWorker")
            .WithCronSchedule(cronSchedule, cronOpts =>
            {
                cronOpts.WithMisfireHandlingInstructionDoNothing(); // Optional
            }));
    });

    // Add hosted service
    builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

    // Add services to the container.
    builder.Services.AddScoped<IAudioFileService, AudioFileService>();
   


    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddInfrastrucureService();
    builder.Services.AddApplicationService();

    builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Emertec")));
    builder.Services.Configure<AudioPaths>(builder.Configuration.GetSection("AudioEncryptionPaths"));
    builder.Services.Configure<MP3Settings>(builder.Configuration.GetSection("MP3Settings"));



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


