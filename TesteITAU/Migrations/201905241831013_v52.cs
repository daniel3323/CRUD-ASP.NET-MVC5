namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v52 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conta", "DataLancamento", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conta", "DataLancamento");
        }
    }
}
