namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conta", "NumeroConta", c => c.String());
            DropColumn("dbo.Conta", "Numero");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conta", "Numero", c => c.String());
            DropColumn("dbo.Conta", "NumeroConta");
        }
    }
}
