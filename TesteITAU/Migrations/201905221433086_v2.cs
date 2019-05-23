namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Endereco", "Usuario_ID", c => c.Int());
            CreateIndex("dbo.Endereco", "Usuario_ID");
            AddForeignKey("dbo.Endereco", "Usuario_ID", "dbo.Usuario", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Endereco", "Usuario_ID", "dbo.Usuario");
            DropIndex("dbo.Endereco", new[] { "Usuario_ID" });
            DropColumn("dbo.Endereco", "Usuario_ID");
        }
    }
}
