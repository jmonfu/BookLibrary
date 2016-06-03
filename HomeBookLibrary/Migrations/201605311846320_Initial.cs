using System.Data.Entity.Migrations;

namespace HomeBookLibrary.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String(false)
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Books",
                c => new
                {
                    Id = c.Int(false, true),
                    Title = c.String(false),
                    ISBN = c.Int(false),
                    Summary = c.String(),
                    IsAvailable = c.Boolean(false),
                    AuthorId = c.Int(false),
                    GenreId = c.Int(false)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId, true)
                .ForeignKey("dbo.Genres", t => t.GenreId, true)
                .Index(t => t.AuthorId)
                .Index(t => t.GenreId);

            CreateTable(
                "dbo.Genres",
                c => new
                {
                    Id = c.Int(false, true),
                    Type = c.String(false)
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Loans",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String(false),
                    Surname = c.String(false),
                    DateLoaned = c.DateTime(false),
                    BookId = c.Int(false)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, true)
                .Index(t => t.BookId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Loans", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Loans", new[] {"BookId"});
            DropIndex("dbo.Books", new[] {"GenreId"});
            DropIndex("dbo.Books", new[] {"AuthorId"});
            DropTable("dbo.Loans");
            DropTable("dbo.Genres");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}