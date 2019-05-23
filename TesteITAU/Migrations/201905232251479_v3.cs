namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Conta", new[] { "Usuario_ID" });
            CreateIndex("dbo.Conta", "usuario_ID");
            DropColumn("dbo.Conta", "NumeroConta");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conta", "NumeroConta", c => c.Int(nullable: false));
            DropIndex("dbo.Conta", new[] { "usuario_ID" });
            CreateIndex("dbo.Conta", "Usuario_ID");
        }
    }
}
