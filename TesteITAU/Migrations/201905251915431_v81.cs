namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v81 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lancamento", "TipoLancamento", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lancamento", "TipoLancamento");
        }
    }
}
