namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lancamento",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DataLancamento = c.DateTime(nullable: false),
                        ValorLancamento = c.Double(nullable: false),
                        Conta_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Conta", t => t.Conta_ID)
                .Index(t => t.Conta_ID);
            
            DropColumn("dbo.Conta", "DataLancamento");
            DropColumn("dbo.Conta", "ValorLancamento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conta", "ValorLancamento", c => c.Double(nullable: false));
            AddColumn("dbo.Conta", "DataLancamento", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Lancamento", "Conta_ID", "dbo.Conta");
            DropIndex("dbo.Lancamento", new[] { "Conta_ID" });
            DropTable("dbo.Lancamento");
        }
    }
}
