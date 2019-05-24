namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conta", "TipoLancamento", c => c.String());
            AddColumn("dbo.Conta", "ValorLancamento", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conta", "ValorLancamento");
            DropColumn("dbo.Conta", "TipoLancamento");
        }
    }
}
