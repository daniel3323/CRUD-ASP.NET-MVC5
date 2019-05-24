namespace TesteITAU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v41 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Login", c => c.String());
            AddColumn("dbo.Usuario", "Senha", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "Senha");
            DropColumn("dbo.Usuario", "Login");
        }
    }
}
