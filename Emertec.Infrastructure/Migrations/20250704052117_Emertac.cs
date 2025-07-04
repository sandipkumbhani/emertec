using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroService_Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Emertac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "modelDimAgent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelDimAgent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modelDimCampaign",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Campaignpath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelDimCampaign", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modelDimCompany",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelDimCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modelDimJson",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DapperGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastSyncDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsExecuted = table.Column<bool>(type: "bit", nullable: false),
                    IsRepeat = table.Column<bool>(type: "bit", nullable: false),
                    CallLength = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelDimJson", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modelDimRespondent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelDimRespondent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modelDimTeam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelDimTeam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modelDimTextFull",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JsonGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    campaign_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelDimTextFull", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modelDimTextSentence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JsonGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sentence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Speaker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelDimTextSentence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modelDimWord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JsonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextSentenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoundsLike = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fuzzy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RateProfanity = table.Column<int>(type: "int", nullable: false),
                    RateComplexity = table.Column<int>(type: "int", nullable: false),
                    VoicePrint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<double>(type: "float", nullable: false),
                    EndTime = table.Column<double>(type: "float", nullable: false),
                    Speaker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Probability = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelDimWord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modelUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelUsers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "modelDimAgent");

            migrationBuilder.DropTable(
                name: "modelDimCampaign");

            migrationBuilder.DropTable(
                name: "modelDimCompany");

            migrationBuilder.DropTable(
                name: "modelDimJson");

            migrationBuilder.DropTable(
                name: "modelDimRespondent");

            migrationBuilder.DropTable(
                name: "modelDimTeam");

            migrationBuilder.DropTable(
                name: "modelDimTextFull");

            migrationBuilder.DropTable(
                name: "modelDimTextSentence");

            migrationBuilder.DropTable(
                name: "modelDimWord");

            migrationBuilder.DropTable(
                name: "modelUsers");
        }
    }
}
