namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conta",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Saldo = c.Double(nullable: false),
                        NumeroConta = c.String(),
                        Usuario_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuario", t => t.Usuario_ID, cascadeDelete: true)
                .Index(t => t.Usuario_ID);
            
            CreateTable(
                "dbo.Lancamento",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Tipo = c.String(),
                        Valor = c.Double(nullable: false),
                        Conta_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Conta", t => t.Conta_ID, cascadeDelete: true)
                .Index(t => t.Conta_ID);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Sobrenome = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Telefone = c.String(),
                        Login = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                        CEP = c.String(nullable: false),
                        Logradouro = c.String(),
                        Numero = c.Int(nullable: false),
                        Complemento = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conta", "Usuario_ID", "dbo.Usuario");
            DropForeignKey("dbo.Lancamento", "Conta_ID", "dbo.Conta");
            DropIndex("dbo.Lancamento", new[] { "Conta_ID" });
            DropIndex("dbo.Conta", new[] { "Usuario_ID" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Lancamento");
            DropTable("dbo.Conta");
        }
    }
}
