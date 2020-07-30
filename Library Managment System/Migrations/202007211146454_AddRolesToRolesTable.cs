namespace Library_Managment_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRolesToRolesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Roles (Name) VALUES ('Admin')");
            Sql("INSERT INTO Roles (Name) VALUES ('User')");
        }

        public override void Down()
        {
        }
    }
}
