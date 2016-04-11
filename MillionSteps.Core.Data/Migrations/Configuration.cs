using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MillionSteps.Core.Data.Migrations
{
  internal sealed class Configuration : DbMigrationsConfiguration<MillionStepsDbContext>
  {
    public Configuration()
    {
      this.AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(MillionStepsDbContext context)
    {
    }
  }
}
