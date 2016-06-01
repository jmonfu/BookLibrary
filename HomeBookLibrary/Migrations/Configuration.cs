using HomeBookLibrary.Models;

namespace HomeBookLibrary.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HomeBookLibrary.Models.HomeBookLibraryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HomeBookLibrary.Models.HomeBookLibraryContext context)
        {
            context.Authors.AddOrUpdate(x => x.Id,
                    new Author() { Id = 1, Name = "William Shakespear" },
                    new Author() { Id = 2, Name = "Dan Brown" },
                    new Author() { Id = 3, Name = "Stephen King" }
            );

            context.Genres.AddOrUpdate(x => x.Id,
                    new Genre() { Id = 1, Type = "Drama"},
                    new Genre() { Id = 2, Type = "Romance" },
                    new Genre() { Id = 3, Type = "Thriller" }
            );

            context.Books.AddOrUpdate(x => x.Id,
                    new Book()
                    {
                        Id = 1,
                        Title = "Hamlet",
                        ISBN = 1853260096,
                        AuthorId = 1,
                        GenreId = 1,
                        IsAvailable = true,
                        Summary = "Hamlet is not only one of Shakespeare's greatest plays, but also the most fascinatingly problematical tragedy in world literature. First performed around 1600, this a gripping and exuberant drama of revenge, rich in contrasts and conflicts.Its violence alternates with introspection, its melancholy with humour, and its subtlety with spectacle.The Prince, Hamlet himself, is depicted as a complex, divided, introspective character.  His reflections on death, morality and the very status of human beings make him  the first modern man. Countless stage productions and numerous adaptations for the  cinema and television have demonstrated the continuing cultural relevance of this vivid,  enigmatic, profound and engrossing drama."
                    },
                    new Book()
                    {
                        Id = 2,
                        Title = "Macbeth",
                        ISBN = 1853260355,
                        AuthorId = 1,
                        GenreId = 1,
                        IsAvailable = true,
                        Summary = "Shakespeare’s Macbeth is one of the greatest tragic dramas the world has known. Macbeth himself,  a brave warrior, is fatally impelled by supernatural forces, by his proud wife, and by his  own burgeoning ambition. As he embarks on his murderous course to gain and retain the crown  of Scotland, we see the appalling emotional and psychological effects on both Lady Macbeth and himself. The cruel ironies of their destiny are conveyed in poetry of unsurpassed power. In the theatre, this tragedy remains perennially engrossing."
                    },
                    new Book()
                    {
                        Id = 3,
                        Title = "Romeo And Juliet",
                        ISBN = 1840224339,
                        AuthorId = 1,
                        GenreId = 2,
                        IsAvailable = true,
                        Summary = "Romeo and Juliet is the world's most famous drama of tragic young love. Defying the feud which divides their families, Romeo and Juliet enjoy the fleeting rapture of courtship, marriage and sexual fulfilment; but a combination of old animosities and new coincidences brings them to suicidal deaths. This play offers a rich mixture of romantic lyricism, bawdy comedy, intimate harmony and sudden violence.Long successful in the theatre, it has also generated numerous operas, ballets and films; and these have helped to make Romeo and Juliet perennially topical."
                    },
                    new Book()
                    {
                        Id = 4,
                        Title = "The Da Vinci Code",
                        ISBN = 0552159719,
                        AuthorId = 2,
                        GenreId = 3,
                        IsAvailable = true,
                        Summary = "Harvard professor Robert Langdon receives an urgent late-night phone call while on business in Paris: the elderly curator of the Louvre has been brutally murdered inside the museum. Alongside the body, police have found a series of baffling codes. As Langdon and a gifted French cryptologist, Sophie Neveu, begin to sort through the bizarre riddles, they are stunned to find a trail that leads to the works of Leonardo Da Vinci - and suggests the answer to a mystery that stretches deep into the vault of history. Unless Langdon and Neveu can decipher the labyrinthine code and quickly assemble the pieces of the puzzle, a stunning historical truth will be lost forever..."
                    },
                    new Book()
                    {
                        Id = 5,
                        Title = "Angels And Demons",
                        ISBN = 0552160899,
                        AuthorId = 2,
                        GenreId = 3,
                        IsAvailable = true,
                        Summary = "The Vatican, Rome: the College of Cardinals assembles to elect a new pope. Somewhere beneath them, an unstoppable bomb of terrifying power relentlessly counts down to oblivion.  In a breathtaking race against time, Harvard professor Robert Langdon must decipher a labyrinthine trail of ancient symbols if he is to defeat those responsible - the Illuminati, a secret brotherhood presumed extinct for nearly four hundred years, reborn to continue their deadly vendetta against their most hated enemy, the Catholic Church."
                    },
                    new Book()
                    {
                        Id = 6,
                        Title = "Finders Kreepers",
                        ISBN = 1473698995,
                        AuthorId = 3,
                        GenreId = 3,
                        IsAvailable = true,
                        Summary = "'Wake up, genius.' So begins King's instantly riveting story about a vengeful reader. The genius is John Rothstein, a Salinger-like icon who created a famous character, Jimmy Gold, but who hasn't published a book for decades. Morris Bellamy is livid, not just because Rothstein has stopped providing books, but because the nonconformist Jimmy Gold has sold out for a career in advertising. Morris kills Rothstein and empties his safe of cash, yes, but the real treasure is a trove of notebooks containing at least one more Gold novel."
                    },
                    new Book()
                    {
                        Id = 7,
                        Title = "Bazaar of Bad Dreams",
                        ISBN = 1473698995,
                        AuthorId = 3,
                        GenreId = 3,
                        IsAvailable = true,
                        Summary = "A generous collection of thrilling stories - some brand new, some published in magazines, all entirely brilliant and assembled in one book for the first time - with a wonderful bonus: in addition to his introduction to the whole collection, King gives readers a fascinating introduction to each story with autobiographical comments on their origins and motivation..."
                    }
            );
        }
    }
}
