namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Endereco", "usuario_ID", "dbo.Usuario");
            DropIndex("dbo.Endereco", new[] { "usuario_ID" });
            DropColumn("dbo.Endereco", "usuario_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Endereco", "usuario_ID", c => c.Int());
            CreateIndex("dbo.Endereco", "usuario_ID");
            AddForeignKey("dbo.Endereco", "usuario_ID", "dbo.Usuario", "ID");
        }
    }
}
