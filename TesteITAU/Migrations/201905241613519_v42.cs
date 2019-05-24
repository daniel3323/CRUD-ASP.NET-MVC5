namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v42 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Conta", "saldo", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Conta", "saldo", c => c.Single(nullable: false));
        }
    }
}
