namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v71 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Conta", "TipoLancamento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conta", "TipoLancamento", c => c.String());
        }
    }
}
