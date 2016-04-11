using System;
using System.Data.Entity.Migrations;

namespace MillionSteps.Core.Data.Migrations
{
  public partial class CreateActivityLogEntriesTableMigration : DbMigration
  {
    public override void Up()
    {
      this.CreateTable("dbo.ActivityLogEntries",
                       c => new {
                         Id = c.Int(nullable: false, identity: true),
                         UserId = c.String(maxLength: 16),
                         Date = c.DateTime(nullable: false),
                         Steps = c.Int(nullable: false),
                       })
          .PrimaryKey(t => t.Id)
          .Index(t => t.UserId);
    }

    public override void Down()
    {
      this.DropIndex("dbo.ActivityLogEntries", new[] { "UserId" });
      this.DropTable("dbo.ActivityLogEntries");
    }
  }
}
