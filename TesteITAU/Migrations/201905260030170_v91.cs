namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v91 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lancamento", "Conta_ID", "dbo.Conta");
            DropForeignKey("dbo.Conta", "Usuario_ID", "dbo.Usuario");
            DropIndex("dbo.Conta", new[] { "Usuario_ID" });
            DropIndex("dbo.Lancamento", new[] { "Conta_ID" });
            AlterColumn("dbo.Conta", "Usuario_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Lancamento", "Conta_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Conta", "Usuario_ID");
            CreateIndex("dbo.Lancamento", "Conta_ID");
            AddForeignKey("dbo.Lancamento", "Conta_ID", "dbo.Conta", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Conta", "Usuario_ID", "dbo.Usuario", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conta", "Usuario_ID", "dbo.Usuario");
            DropForeignKey("dbo.Lancamento", "Conta_ID", "dbo.Conta");
            DropIndex("dbo.Lancamento", new[] { "Conta_ID" });
            DropIndex("dbo.Conta", new[] { "Usuario_ID" });
            AlterColumn("dbo.Lancamento", "Conta_ID", c => c.Int());
            AlterColumn("dbo.Conta", "Usuario_ID", c => c.Int());
            CreateIndex("dbo.Lancamento", "Conta_ID");
            CreateIndex("dbo.Conta", "Usuario_ID");
            AddForeignKey("dbo.Conta", "Usuario_ID", "dbo.Usuario", "ID");
            AddForeignKey("dbo.Lancamento", "Conta_ID", "dbo.Conta", "ID");
        }
    }
}
