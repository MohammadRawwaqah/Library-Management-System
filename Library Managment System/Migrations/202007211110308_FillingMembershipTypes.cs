namespace Library_Managment_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FillingMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MembershipTypes (Id, Name, DiscountRate) VALUES (1, 'Pay as you go', 0)");
            Sql("INSERT INTO MembershipTypes (Id, Name, DiscountRate) VALUES (2, 'Monthly', 15)");
            Sql("INSERT INTO MembershipTypes (Id, Name, DiscountRate) VALUES (3, 'Annually', 30)");

        }

        public override void Down()
        {
        }
    }
}
