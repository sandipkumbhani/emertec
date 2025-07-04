using MicroService_Template.Domain.Model;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContext) : base(dbContext)
    {

    }
    public DbSet<ModelDimJson> modelDimJson { get; set; }
    public object ModelDimJson { get; internal set; }
    public DbSet<ModelDimCompany> modelDimCompany { get; set; }
    public DbSet<ModelDimCampaign> modelDimCampaign { get; set; }
    public DbSet<ModelDimTextSentence> modelDimTextSentence { get; set; }
    public DbSet<ModelDimTextFull> modelDimTextFull { get; set; }
    public DbSet<ModelDimWord> modelDimWord { get; set; }
    public DbSet<ModelDimAgent> modelDimAgent { get; set; }
    public DbSet<ModelDimTeam> modelDimTeam { get; set; }
    public DbSet<ModelDimRespondent> modelDimRespondent { get; set; }
    public DbSet<ModelUserLogin> modelUsers { get; set; }


}