insert into MediaLib.dbo.UserSet (Username, Password)
values ('test@test.com', 'pwd');

insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
values ('Der Terminator', 'The Terminator', 'Action', 3.44, 'false');

insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
values ('The Texas Chainsaw Massacre', 'The Texas Chainsaw Massacre', 'Horror', 5, 'false');

insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
values ('Teenage Mutant Hero Turtles', 'Tennage Mutant Ninja Turtles', 'Action', 2, 'false');

insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
values ('Rhea M. - Es begann ohne Warnung', 'Maximum Overdrive', 'Horror', 3.33, 'false');

insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
values ('Shining', 'The Shining', 'Horror', 4.12, 'false');

insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
values ('South Park', 'South Park', 'Comedy', 5, 'false');

insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
values ('Faust. Der Tragödie erster Teil', 'Faust. Der Tragödie erster Teil', 'Drama', 4.4, 'false');

insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
values ('How I Met Your Mother', 'How I Met Your Mother', 'Comedy', 5, 'false');

insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
values ('In einer kleinen Stadt', 'Needful Things', 'Horror', 2.41, 'false');

insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
values ('Der Herr der Ringe - Die Gefährten', 'The Lord of the Rings - The Fellowship of the Ring', 'Fantasy', 3.02, 'false');

insert into MediaLib.dbo.MediaSet_Video (Id)
values(1);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(2);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(3);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(4);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(5);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(6);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(8);
 
insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(1, '1984-10-26');

insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(2, '1974-10-01');

insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(3, '1990-03-30');

insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(4, '1986-07-25');

insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(5, '1980-05-23');

insert into MediaLib.dbo.MediaSet_TV_Show (Id, ShowBeginning)
values(6, '1997-08-13');

insert into MediaLib.dbo.MediaSet_TV_Show (Id, ShowBeginning)
values(8, '2005-09-19');

insert into MediaLib.dbo.SeasonSet (Number, TV_ShowId)
values(1, 6);

insert into MediaLib.dbo.SeasonSet (Number, TV_ShowId)
values(2, 6);

insert into MediaLib.dbo.SeasonSet (Number, TV_ShowId)
values(1, 8);

insert into MediaLib.dbo.SeasonSet (Number, TV_ShowId)
values(2, 8);

insert into MediaLib.dbo.EpisodeSet (Name, SeasonId)
values('Timmy 2000', 1);

insert into MediaLib.dbo.EpisodeSet (Name, SeasonId)
values('Cartman''s Mom Is Still a Dirty Slut', 2);

insert into MediaLib.dbo.EpisodeSet (Name, SeasonId)
values('Make Love, Not Warcraft', 2);

insert into MediaLib.dbo.EpisodeSet (Name, SeasonId)
values('Arrivederci, Fiero', 3);

insert into MediaLib.dbo.EpisodeSet (Name, SeasonId)
values('Doppelgangers', 4);

insert into MediaLib.dbo.MediaSet_Book (Id, Isbn)
values(7, '817525766-0');

insert into MediaLib.dbo.MediaSet_Book (Id, Isbn)
values(9, '654868498-6');

insert into MediaLib.dbo.MediaSet_Book (Id, Isbn)
values(10, '648694653-5');

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('I''ll be back', 'The Terminator', 'English', 1, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('Shut up you bitch hog!', 'Old Man', 'English', 2, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('I made a funny.', 'Splinter', 'English', 3, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('Adios mother fucker!', 'Bill Robinson', 'English', 0, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('If you don''t get your hand off my leg, 
you''re going to be wiping your ass with a hook next time you take a dump!', 'Brett', 'English', 3, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('Heeere''s Johnny!', 'Jack Torrance', 'English', 5, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('Habe nun, ach! Philosophie,
Juristerei und Medizin
Und leider auch Theologie
Durchaus studiert, mit heißem Bemühn.
Da steh ich nun, ich armer Tor!
Und bin so klug als wie zuvor;', 'Heinrich Faust', 'Deutsch', 1, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('Nein, nein! der Teufel ist ein Egoist
Und tut nicht leicht um Gottes willen,
Was einem andern nützlich ist.', 'Mephisto', 'Deutsch', 0, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('TIMAH.', 'Timmy', 'English', 10, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('Drugs are bad, mkay ...', 'Mr. Mackey', 'English', 4, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('How do you kill, that which has no life? ', 'Blizzard Employee', 'English', 5, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('Moist', 'Barney Stinson', 'English', 6, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('You son of a bitch!', 'Lily Aldrin', 'English', 7, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('It''s aggravations I mostly want to talk about-- can you sit a spell with me?', 'Old-Timer', 'English', 0, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('I don''t know half of you half as well as I should like; 
and I like less than half of you half as well as you deserve.', 'Bilbo Baggins', 'English', 6, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('Not yet. Not for about 40 years.', 'Kyle Reese', 'English', 2, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('Reese. Why me? Why does it want me?', 'Sarah Connor', 'English', 8, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('Your clothes... give them to me, now.', 'The Terminator', 'English', 6, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('The film which you are about to see is an account of the tragedy which befell a group of five youths, 
in particular Sally Hardesty and her invalid brother, Franklin. It is all the more tragic in that they were young. 
But, had they lived very, very long lives, they could not have expected nor would they have wished to see as much of 
the mad and macabre as they were to see that day. For them an idyllic summer afternoon drive became a nightmare. 
The events of that day were to lead to the discovery of one of the most bizarre crimes in the annals of American history, 
The Texas Chain Saw Massacre.', 'Narrator', 'English', 4, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('Have you been doing those Reader''s Digest ''Word-Power'' columns again?', 'Jerry', 'English', 0, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('Honey! C''mon over here, Sugar-buns. This machine just called me an asshole!', 'Man At Cashpoint', 'English', 9, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId)
values ('Jesus is coming and he is pissed! ', 'Bill Robinson', 'English', 1, 1);

insert into MediaLib.dbo.QuoteSet_QuoteMovie (Id, MovieId)
values (1, 1);

insert into MediaLib.dbo.QuoteSet_QuoteMovie (Id, MovieId)
values (2, 2);

insert into MediaLib.dbo.QuoteSet_QuoteMovie (Id, MovieId)
values (3, 3);

insert into MediaLib.dbo.QuoteSet_QuoteMovie (Id, MovieId)
values (4, 4);

insert into MediaLib.dbo.QuoteSet_QuoteMovie (Id, MovieId)
values (5, 4);

insert into MediaLib.dbo.QuoteSet_QuoteMovie (Id, MovieId)
values (6, 5);

insert into MediaLib.dbo.QuoteSet_QuoteBook (Id, BookId)
values (7, 7);

insert into MediaLib.dbo.QuoteSet_QuoteBook (Id, BookId)
values (8, 7);

insert into MediaLib.dbo.QuoteSet_QuoteTV_Show(Id, EpisodeId)
values (9, 1);

insert into MediaLib.dbo.QuoteSet_QuoteTV_Show(Id, EpisodeId)
values (10, 2);

insert into MediaLib.dbo.QuoteSet_QuoteTV_Show(Id, EpisodeId)
values (11, 3);

insert into MediaLib.dbo.QuoteSet_QuoteTV_Show(Id, EpisodeId)
values (12, 4);

insert into MediaLib.dbo.QuoteSet_QuoteTV_Show(Id, EpisodeId)
values (13, 5);

insert into MediaLib.dbo.QuoteSet_QuoteBook(Id, BookId)
values (14, 9);

insert into MediaLib.dbo.QuoteSet_QuoteBook(Id, BookId)
values (15, 10);

insert into MediaLib.dbo.QuoteSet_QuoteMovie(Id, MovieId)
values (16, 1);

insert into MediaLib.dbo.QuoteSet_QuoteMovie(Id, MovieId)
values (17, 1);

insert into MediaLib.dbo.QuoteSet_QuoteMovie(Id, MovieId)
values (18, 1);

insert into MediaLib.dbo.QuoteSet_QuoteMovie(Id, MovieId)
values (19, 2);

insert into MediaLib.dbo.QuoteSet_QuoteMovie(Id, MovieId)
values (20, 2);

insert into MediaLib.dbo.QuoteSet_QuoteMovie(Id, MovieId)
values (21, 4);

insert into MediaLib.dbo.QuoteSet_QuoteMovie(Id, MovieId)
values (22, 4);