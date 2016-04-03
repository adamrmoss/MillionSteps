using System;
using System.Data.Entity.Migrations;

namespace MillionSteps.Core.Data.Migrations
{
  public partial class InitialMigration : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.UserSessions",
          c => new {
            Id = c.Guid(nullable: false),
            TempToken = c.String(maxLength: 128),
            TempSecret = c.String(maxLength: 128),
            Verifier = c.String(maxLength: 128),
            Token = c.String(maxLength: 128),
            Secret = c.String(maxLength: 128),
            UserId = c.String(maxLength: 16),
            DateCreated = c.DateTime(nullable: false),
            OffsetFromUtcMillis = c.Int(nullable: false),
          })
          .PrimaryKey(t => t.Id)
          .Index(t => t.TempToken, unique: true)
          .Index(t => t.UserId);

      CreateTable(
          "dbo.ActivityLogEntries",
          c => new {
            Id = c.Int(nullable: false, identity: true),
            UserId = c.String(maxLength: 16),
            Date = c.DateTime(nullable: false),
            Steps = c.Int(nullable: false),
          })
          .PrimaryKey(t => t.Id)
          .Index(t => t.UserId);

      CreateTable(
          "dbo.Adventures",
          c => new {
            Id = c.Int(nullable: false, identity: true),
            UserId = c.String(maxLength: 16),
            DateCreated = c.DateTime(nullable: false),
            CurrentMoment_Id = c.Int(),
          })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Moments", t => t.CurrentMoment_Id)
          .Index(t => t.UserId);

      CreateTable(
          "dbo.Moments",
          c => new {
            Id = c.Int(nullable: false, identity: true),
            EventName = c.String(maxLength: 128),
            StepsConsumed = c.Int(nullable: false),
            Ordinal = c.Int(nullable: false),
            Adventure_Id = c.Int(),
          })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Adventures", t => t.Adventure_Id)
          .Index(t => t.Adventure_Id);

      CreateTable(
          "dbo.MomentFlags",
          c => new {
            Id = c.Int(nullable: false, identity: true),
            Flag = c.String(maxLength: 64),
            Moment_Id = c.Int(),
          })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Moments", t => t.Moment_Id)
          .Index(t => t.Moment_Id);
    }

    public override void Down()
    {
      DropForeignKey("dbo.MomentFlags", "Moment_Id", "dbo.Moments");
      DropIndex("dbo.MomentFlags", new[] { "Moment_Id" });
      DropTable("dbo.MomentFlags");

      DropForeignKey("dbo.Moments", "Adventure_Id", "dbo.Adventures");
      DropIndex("dbo.Moments", new[] { "Adventure_Id" });

      DropForeignKey("dbo.Adventures", "CurrentMoment_Id", "dbo.Moments");
      DropTable("dbo.Moments");

      DropIndex("dbo.Adventures", new[] { "UserId" });
      DropTable("dbo.Adventures");

      DropIndex("dbo.ActivityLogEntries", new[] { "UserId" });
      DropTable("dbo.ActivityLogEntries");

      DropIndex("dbo.UserSessions", new[] { "UserId" });
      DropIndex("dbo.UserSessions", new[] { "TempToken" });
      DropTable("dbo.UserSessions");
    }
  }
}
