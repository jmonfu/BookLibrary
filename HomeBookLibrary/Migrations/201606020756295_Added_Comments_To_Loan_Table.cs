namespace HomeBookLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Comments_To_Loan_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "Comments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loans", "Comments");
        }
    }
}
