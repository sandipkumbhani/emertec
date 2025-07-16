using Emertec.Application.Extension;
using Emertec.Infrastructure.Extension;
using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Application.Services;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Extension.Interface;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Services;
using MicroService_Template.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using System.Text;
//using MicroService_Template.Job;
{

    var builder = WebApplication.CreateBuilder(args);
    //builder.Services.AddQuartz(q =>
    //{
    //    var mp3ToRsaJobKey = new JobKey("MP3ToRSAWorker");
    //    var rsaToJsonJobKey = new JobKey("RSAToJsonWorker");
    //    var jsonToDbJobKey = new JobKey("JsonToDbWorker");

    //    // Register jobs
    //    q.AddJob<MP3ToRSAWorker>(opts => opts.WithIdentity(mp3ToRsaJobKey));
    //    q.AddJob<RSAToJsonWorker>(opts => opts.WithIdentity(rsaToJsonJobKey));
    //    q.AddJob<JsonToDbWorker>(opts => opts.WithIdentity(jsonToDbJobKey));

    //    // Get individual cron expressions
    //    var mp3ToRsaCron = builder.Configuration["Quartz:MP3ToRSAJob"];
    //    var rsaToJsonCron = builder.Configuration["Quartz:RSAToJsonJob"];
    //    var jsonToDbCron = builder.Configuration["Quartz:JsonToDbJob"];

    //    // Add triggers for each job
    //    q.AddTrigger(opts => opts
    //        .ForJob(mp3ToRsaJobKey)
    //        .WithIdentity("TriggerMP3ToRSA")
    //        .WithCronSchedule(mp3ToRsaCron, cron => cron.WithMisfireHandlingInstructionDoNothing()));

    //    q.AddTrigger(opts => opts
    //        .ForJob(rsaToJsonJobKey)
    //        .WithIdentity("TriggerRSAToJson")
    //        .WithCronSchedule(rsaToJsonCron, cron => cron.WithMisfireHandlingInstructionDoNothing()));

    //    q.AddTrigger(opts => opts
    //        .ForJob(jsonToDbJobKey)
    //        .WithIdentity("TriggerJsonToDb")
    //        .WithCronSchedule(jsonToDbCron, cron => cron.WithMisfireHandlingInstructionDoNothing()));
    //});
    //builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);



    //REPOSITORY INJECTION

    builder.Services.AddScoped<IModelDimJsonRepository, ModelDimJsonRepository>();
    builder.Services.AddScoped<IModelDimCompanyRepository, ModelDimCompanyRepository>();
    builder.Services.AddScoped<IModelDimCampaignRepository, ModelDimCampaignRepository>();
    builder.Services.AddScoped<IModelDimTextFullRepository, ModelDimTextFullRepository>();
    builder.Services.AddScoped<IModelDimAgentRepository, ModelDimAgentRepository>();
    builder.Services.AddScoped<IModelDimTextSentenceRepository, IModelDimTextWordRepository>();
    builder.Services.AddScoped<IModelDimTextWordRepositorycs, ModelDimTextWordRepositorycs>();
    builder.Services.AddScoped<IModelUserLoginRepository, ModelUserLoginRepository>();
    builder.Services.AddScoped<IModelCreateUserRepository, ModelCreateUserRepository>();
    builder.Services.AddScoped<IModelMenuMasterRepository, ModelMenuMasterRepository>();
    builder.Services.AddScoped<IModelUserMenuMappingRepository, ModelUserMenuMappingRepository>();
    builder.Services.AddScoped<IModelUserRoleRepository, ModelUserRoleRepository>();


    //SERVICE INJECTION 
    builder.Services.AddScoped<IAudioFileService, AudioFileService>();
    builder.Services.AddScoped<IConvertRsaToJsonService, ConvertRsaToJsonService>();
    builder.Services.AddScoped<ICryptoService, CryptoService>();
    builder.Services.AddScoped<IConvertJsonToDbService, ConvertJsonToDbService>();
    builder.Services.AddScoped<IModelCreateUserService, ModelCreateUserService>();
    builder.Services.AddScoped<IModelMenuMasterService, ModelMenuMasterService>();
    builder.Services.AddScoped<IUserLoginService, UserLoginService>();
    builder.Services.AddScoped<IModelUserMenuMappingService,ModelUserMenuMappingService>();
    builder.Services.AddScoped<IModelUserRoleService, ModelUserRoleService>();


    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddInfrastrucureService();
    builder.Services.AddApplicationService();

    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    //builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.Configure<AudioPathsDTO>(builder.Configuration.GetSection("AudioEncryptionPaths"));
    builder.Services.Configure<MP3SettingsDTO>(builder.Configuration.GetSection("MP3Settings"));
    builder.Services.Configure<DecryptRequestDTO>(builder.Configuration.GetSection("DecryptRequest"));
    builder.Services.Configure<KeyGenerationResponseDTO>(builder.Configuration.GetSection("Crypto"));
    builder.Services.Configure<JsonToDbDTO>(builder.Configuration.GetSection("JsonToDB"));


    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
 .AddJwtBearer(options =>
 {
     options.RequireHttpsMetadata = false; // Set to true in production
     options.SaveToken = true;
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = builder.Configuration["Jwt:Issuer"],
         ValidAudience = builder.Configuration["Jwt:Audience"],
         IssuerSigningKey = new SymmetricSecurityKey(
             Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
     };
 });

    builder.Services.AddAuthorization();
    builder.Services.AddSwaggerGen(option =>
    {
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
                     {
                         {
                               new OpenApiSecurityScheme
                                 {
                                     Reference = new OpenApiReference
                                     {
                                         Type = ReferenceType.SecurityScheme,
                                         Id = "Bearer"
                                     }
                                 },
                                 new string[] {}
                         }
                     });

    });




    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseStaticFiles();

    app.MapControllers();

    

    app.Run();
}


