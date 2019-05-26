namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lancamento", "Data", c => c.DateTime(nullable: false));
            AddColumn("dbo.Lancamento", "Tipo", c => c.String());
            AddColumn("dbo.Lancamento", "Valor", c => c.Double(nullable: false));
            DropColumn("dbo.Lancamento", "DataLancamento");
            DropColumn("dbo.Lancamento", "TipoLancamento");
            DropColumn("dbo.Lancamento", "ValorLancamento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lancamento", "ValorLancamento", c => c.Double(nullable: false));
            AddColumn("dbo.Lancamento", "TipoLancamento", c => c.String());
            AddColumn("dbo.Lancamento", "DataLancamento", c => c.DateTime(nullable: false));
            DropColumn("dbo.Lancamento", "Valor");
            DropColumn("dbo.Lancamento", "Tipo");
            DropColumn("dbo.Lancamento", "Data");
        }
    }
}
