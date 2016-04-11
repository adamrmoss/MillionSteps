using System;
using System.Data.Entity.Migrations;

namespace MillionSteps.Core.Data.Migrations
{
  public partial class CreateUserSessionsTableMigration : DbMigration
  {
    public override void Up()
    {
      this.CreateTable("dbo.UserSessions",
                       c => new {
                         Id = c.Guid(nullable: false),
                         Verifier = c.Guid(nullable: false),
                         AccessToken = c.String(maxLength: 256),
                         RefreshToken = c.String(maxLength: 256),
                         RedirectUrl = c.String(maxLength: 256),
                         UserId = c.String(maxLength: 16),
                         DateCreated = c.DateTime(nullable: false),
                         OffsetFromUtcMillis = c.Int(nullable: false),
                       })
          .PrimaryKey(t => t.Id)
          .Index(t => t.Verifier, unique: true)
          .Index(t => t.UserId);
    }

    public override void Down()
    {
      this.DropIndex("dbo.UserSessions", new[] { "UserId" });
      this.DropIndex("dbo.UserSessions", new[] { "Verifier" });
      this.DropTable("dbo.UserSessions");
    }
  }
}
