namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Endereco", "Usuario_ID", "dbo.Usuario");
            DropIndex("dbo.Endereco", new[] { "Usuario_ID" });
            AddColumn("dbo.Usuario", "CEP", c => c.String());
            AddColumn("dbo.Usuario", "Logradouro", c => c.String());
            AddColumn("dbo.Usuario", "Numero", c => c.Int(nullable: false));
            AddColumn("dbo.Usuario", "Complemento", c => c.String());
            AddColumn("dbo.Usuario", "Bairro", c => c.String());
            AddColumn("dbo.Usuario", "Cidade", c => c.String());
            AddColumn("dbo.Usuario", "Estado", c => c.String());
            DropTable("dbo.Endereco");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CEP = c.String(),
                        Logradouro = c.String(),
                        Numero = c.Int(nullable: false),
                        Complemento = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        Estado = c.String(),
                        Usuario_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Usuario", "Estado");
            DropColumn("dbo.Usuario", "Cidade");
            DropColumn("dbo.Usuario", "Bairro");
            DropColumn("dbo.Usuario", "Complemento");
            DropColumn("dbo.Usuario", "Numero");
            DropColumn("dbo.Usuario", "Logradouro");
            DropColumn("dbo.Usuario", "CEP");
            CreateIndex("dbo.Endereco", "Usuario_ID");
            AddForeignKey("dbo.Endereco", "Usuario_ID", "dbo.Usuario", "ID");
        }
    }
}
