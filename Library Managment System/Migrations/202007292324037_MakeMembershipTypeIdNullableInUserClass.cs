namespace Library_Managment_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeMembershipTypeIdNullableInUserClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "MembershipTypeId", "dbo.MembershipTypes");
            DropIndex("dbo.Users", new[] { "MembershipTypeId" });
            AlterColumn("dbo.Users", "MembershipTypeId", c => c.Byte());
            CreateIndex("dbo.Users", "MembershipTypeId");
            AddForeignKey("dbo.Users", "MembershipTypeId", "dbo.MembershipTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "MembershipTypeId", "dbo.MembershipTypes");
            DropIndex("dbo.Users", new[] { "MembershipTypeId" });
            AlterColumn("dbo.Users", "MembershipTypeId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Users", "MembershipTypeId");
            AddForeignKey("dbo.Users", "MembershipTypeId", "dbo.MembershipTypes", "Id", cascadeDelete: true);
        }
    }
}
