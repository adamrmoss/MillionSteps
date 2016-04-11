using System;
using System.Data.Entity.Migrations;

namespace MillionSteps.Core.Data.Migrations
{
  public partial class CreateAdventureTablesMigration : DbMigration
  {
    public override void Up()
    {
      this.CreateTable("dbo.Adventures",
                       c => new {
                         Id = c.Int(nullable: false, identity: true),
                         UserId = c.String(maxLength: 16),
                         DateCreated = c.DateTime(nullable: false),
                         CurrentMomentId = c.Int(nullable: false),
                       })
          .PrimaryKey(t => t.Id)
          .Index(t => t.UserId);

      this.CreateTable("dbo.Moments",
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

      this.CreateTable("dbo.MomentFlags",
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
      this.DropForeignKey("dbo.MomentFlags", "Moment_Id", "dbo.Moments");
      this.DropIndex("dbo.MomentFlags", new[] { "Moment_Id" });
      this.DropTable("dbo.MomentFlags");

      this.DropForeignKey("dbo.Moments", "Adventure_Id", "dbo.Adventures");
      this.DropIndex("dbo.Moments", new[] { "Adventure_Id" });
      this.DropTable("dbo.Moments");

      this.DropIndex("dbo.Adventures", new[] { "UserId" });
      this.DropTable("dbo.Adventures");
    }
  }
}
