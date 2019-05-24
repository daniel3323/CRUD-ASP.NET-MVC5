namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Conta", new[] { "usuario_ID" });
            CreateIndex("dbo.Conta", "Usuario_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Conta", new[] { "Usuario_ID" });
            CreateIndex("dbo.Conta", "usuario_ID");
        }
    }
}
