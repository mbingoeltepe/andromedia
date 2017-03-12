SET DATEFORMAT ymd;

insert into MediaLib.dbo.UserSet (Username, Password, Verified, Closed)
values ('test@test.com', '1882486413273131145710119411619223124721221895164172216', 'true', 'false');

insert into MediaLib.dbo.UserSet (Username, Password, Verified, Closed)
values ('testEins@test.com', '1882486413273131145710119411619223124721221895164172216', 'true', 'false');

insert into MediaLib.dbo.UserSet (Username, Password, Verified, Closed)
values ('testZwei@test.com', '1882486413273131145710119411619223124721221895164172216', 'true', 'false');

insert into MediaLib.dbo.UserSet (Username, Password, Verified, Closed)
values ('testDrei@test.com', '1882486413273131145710119411619223124721221895164172216', 'true', 'false');

insert into MediaLib.dbo.UserSet (Username, Password, Verified, Closed)
values ('other@test.com', '2472191425412125053136311361012361647020018015323624339', 'true', 'false');

insert into MediaLib.dbo.UserSet (Username, Password, Verified, Closed)
values ('admin@andromedia.com', '6016863931261801532213214170911722181572352028119241', 'true', 'false');

insert into MediaLib.dbo.UserSet_Admin (Id)
values(6);
--1
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('Der Terminator', 'The Terminator', 'Action', 4, 2, 2, 'false', '2011-01-01');

--2
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('The Texas Chainsaw Massacre - Blutgericht in Texas', 'The Texas Chainsaw Massacre', 'Horror', 9, 3, 3, 'false', '2011-05-23');

--3
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('Turtles - Der Film', 'Tennage Mutant Ninja Turtles', 'Action', 8, 4, 2, 'false','2011-03-02');

--4
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('Rhea M. - Es begann ohne Warnung', 'Maximum Overdrive', 'Horror', 25, 5, 5, 'false','2011-06-02');

--5
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('Shining', 'The Shining', 'Horror', 4, 2, 2, 'false','2011-03-29');

--6
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('South Park', 'South Park', 'Comedy', 10, 2, 5, 'false','2011-04-02');

--7
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('Faust. Der Tragödie erster Teil', 'Faust. Der Tragödie erster Teil', 'Drama', 9, 2, 4.5, 'false','2011-03-02');

--8
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('How I Met Your Mother', 'How I Met Your Mother', 'Comedy', 7, 2, 3.5, 'false','2011-03-02');

--9
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('In einer kleinen Stadt', 'Needful Things', 'Horror', 2, 1, 2, 'false','2011-05-12');

--10
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('Der Herr der Ringe - Die Gefährten', 'The Lord of the Rings - The Fellowship of the Ring', 'Fantasy', 3, 3, 1, 'false','2011-03-02');

--11
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('Fake Movie', 'Fake Movie', 'Fantasy', 5, 2, 2.5, 'false','2011-01-02');

--12
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('Fake Movie', 'Fake Movie', 'Fantasy', 5, 2, 2.5, 'false','2011-05-16');

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

insert into MediaLib.dbo.MediaSet_Video (Id)
values(11);
 
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

insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(11, '1984-09-07');

insert into MediaLib.dbo.MediaSet_TV_Show (Id, ShowBeginning)
values(6, '1997-08-13');

insert into MediaLib.dbo.MediaSet_TV_Show (Id, ShowBeginning)
values(8, '2005-09-19');

insert into MediaLib.dbo.SeasonSet (Number, TV_ShowId)
values(2, 6);

insert into MediaLib.dbo.SeasonSet (Number, TV_ShowId)
values(1, 6);

insert into MediaLib.dbo.SeasonSet (Number, TV_ShowId)
values(2, 8);

insert into MediaLib.dbo.SeasonSet (Number, TV_ShowId)
values(1, 8);

insert into MediaLib.dbo.EpisodeSet (Name, SeasonId, Number)
values('Timmy 2000', 1, 1);

insert into MediaLib.dbo.EpisodeSet (Name, SeasonId, Number)
values('Cartman''s Mom Is Still a Dirty Slut', 2, 2);

insert into MediaLib.dbo.EpisodeSet (Name, SeasonId, Number)
values('Make Love, Not Warcraft', 2, 2);

insert into MediaLib.dbo.EpisodeSet (Name, SeasonId,  Number)
values('Arrivederci, Fiero', 3, 3);

insert into MediaLib.dbo.EpisodeSet (Name, SeasonId,Number )
values('Doppelgangers', 4, 4);

insert into MediaLib.dbo.MediaSet_Book (Id, Isbn)
values(7, '817525766-0');

insert into MediaLib.dbo.MediaSet_Book (Id, Isbn)
values(9, '654868498-6');

insert into MediaLib.dbo.MediaSet_Book (Id, Isbn)
values(10, '648694653-5');

insert into MediaLib.dbo.MediaSet_Book (Id, Isbn)
values(12, '777777774-5');

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('I''ll be back', 'The Terminator', 'English', 1, 1, 1, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('Shut up you bitch hog!', 'Old Man', 'English', 2, 1, 2, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('I made a funny.', 'Splinter', 'English', 3, 1, 3, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('Adios mother fucker!', 'Bill Robinson', 'English', 0, 1, 4, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('If you don''t get your hand off my leg, you''re going to be wiping your ass with a hook next time you take a dump!', 'Brett', 'English', 3, 1, 4, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('Heeere''s Johnny!', 'Jack Torrance', 'English', 5, 1, 5, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('Habe nun, ach! Philosophie, Juristerei und Medizin Und leider auch Theologie Durchaus studiert, mit heißem Bemühn. Da steh ich nun, ich armer Tor! Und bin so klug als wie zuvor;', 'Heinrich Faust', 'Deutsch', 1, 1, 7, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('Nein, nein! der Teufel ist ein Egoist Und tut nicht leicht um Gottes willen, Was einem andern nützlich ist.', 'Mephisto', 'Deutsch', 0, 1, 7, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('TIMAH.', 'Timmy', 'English', 10, 1, 6, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('Drugs are bad, mkay ...', 'Mr. Mackey', 'English', 4, 1, 6, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('How do you kill, that which has no life? ', 'Blizzard Employee', 'English', 5, 1, 6, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('Moist', 'Barney Stinson', 'English', 6, 1, 8, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('You son of a bitch!', 'Lily Aldrin', 'English', 7, 1, 8, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('It''s aggravations I mostly want to talk about-- can you sit a spell with me?', 'Old-Timer', 'English', 0, 1, 9, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('I don''t know half of you half as well as I should like; and I like less than half of you half as well as you deserve.', 'Bilbo Baggins', 'English', 6, 1, 10, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('Not yet. Not for about 40 years.', 'Kyle Reese', 'English', 2, 1, 1, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('Reese. Why me? Why does it want me?', 'Sarah Connor', 'English', 8, 1, 1, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('Your clothes... give them to me, now.', 'The Terminator', 'English', 6, 1, 1, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('The film which you are about to see is an account of the tragedy which befell a group of five youths, 
in particular Sally Hardesty and her invalid brother, Franklin. It is all the more tragic in that they were young. 
But, had they lived very, very long lives, they could not have expected nor would they have wished to see as much of 
the mad and macabre as they were to see that day. For them an idyllic summer afternoon drive became a nightmare. 
The events of that day were to lead to the discovery of one of the most bizarre crimes in the annals of American history, 
The Texas Chain Saw Massacre.', 'Narrator', 'English', 4, 1, 2, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('Have you been doing those Reader''s Digest ''Word-Power'' columns again?', 'Jerry', 'English', 0, 1, 2, 1);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('Honey! C''mon over here, Sugar-buns. This machine just called me an asshole!', 'Man At Cashpoint', 'English', 9, 1, 4, 1);

--22
insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('Jesus is coming and he is pissed! ', 'Bill Robinson', 'English', 1, 1, 4, 1);


/* DummyQuote */
insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('DummyQuote', 'The Terminator', 'English', 1, 1, 1, 0);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('DummyQuote1', 'The Terminator', 'English', 1, 1, 2, 0);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('DummyQuote2', 'The Terminator', 'English', 1, 2, 3, 0);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('DummyQuote3', 'The Terminator', 'English', 1, 3, 3, 0);

insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
values ('DummyQuote3', 'The Terminator', 'English', 1, 3, 4, 0);


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

insert into MediaLib.dbo.QuoteSet_QuoteMovie(Id, MovieId)
values (23, 1);

insert into MediaLib.dbo.QuoteSet_QuoteMovie(Id, MovieId)
values (24, 2);

insert into MediaLib.dbo.QuoteSet_QuoteMovie(Id, MovieId)
values (25, 3);

insert into MediaLib.dbo.QuoteSet_QuoteMovie(Id, MovieId)
values (26, 3);

insert into MediaLib.dbo.QuoteSet_QuoteMovie(Id, MovieId)
values (27, 4);


/************* UserMedia **********************************/

insert into MediaLib.dbo.UserMediaSet(StoragePlace, MediaStatus, UserId)
values ('Keller', 'NichtVerborgt', 1);

insert into MediaLib.dbo.UserMediaSet(StoragePlace, MediaStatus, UserId)
values ('Dachboden', 'NichtVerborgt', 1);

insert into MediaLib.dbo.UserMediaSet(StoragePlace, MediaStatus, UserId)
values ('Andere', 'NichtVerborgt', 1);

insert into MediaLib.dbo.UserMediaSet(StoragePlace, MediaStatus, UserId)
values ('Keller', 'NichtVerborgt', 1);

insert into MediaLib.dbo.UserMediaSet(StoragePlace, MediaStatus, UserId)
values ('Schlafzimmer', 'NichtVerborgt', 1);

insert into MediaLib.dbo.UserMediaSet(StoragePlace, MediaStatus, UserId)
values ('Arbeitszimmer', 'NichtVerborgt', 1);

insert into MediaLib.dbo.UserMediaSet(StoragePlace, MediaStatus, UserId)
values ('Kinderzimmer', 'NichtVerborgt', 1);

insert into MediaLib.dbo.UserMediaSet(StoragePlace, MediaStatus, UserId)
values ('Küche', 'NichtVerborgt', 1);

insert into MediaLib.dbo.UserMediaSet(StoragePlace, MediaStatus, UserId)
values ('Dachboden', 'NichtVerborgt', 1);

insert into MediaLib.dbo.UserMediaSet_UserBook(Id, BookId)
values (1, 7);

insert into MediaLib.dbo.UserMediaSet_UserBook(Id, BookId)
values (2, 7);

insert into MediaLib.dbo.UserMediaSet_UserBook(Id, BookId)
values (3, 7);

insert into MediaLib.dbo.UserMediaSet_UserVideo(Id, StorageType)
values(4, 'Dvd');

insert into MediaLib.dbo.UserMediaSet_UserVideo(Id, StorageType)
values(5, 'Vhs');

insert into MediaLib.dbo.UserMediaSet_UserVideo(Id, StorageType)
values(6, 'BluRay');

insert into MediaLib.dbo.UserMediaSet_UserVideo(Id, StorageType)
values(7, 'Dvd');

insert into MediaLib.dbo.UserMediaSet_UserVideo(Id, StorageType)
values(8, 'Vhs');

insert into MediaLib.dbo.UserMediaSet_UserVideo(Id, StorageType)
values(9, 'BluRay');

insert into MediaLib.dbo.UserMediaSet_UserMovie(Id, MovieId)
values(4, 1);

insert into MediaLib.dbo.UserMediaSet_UserMovie(Id, MovieId)
values(5, 1);

insert into MediaLib.dbo.UserMediaSet_UserMovie(Id, MovieId)
values(6, 1);

insert into MediaLib.dbo.UserMediaSet_UserTV_Show(Id, SeasonId)
values(7, 1);

insert into MediaLib.dbo.UserMediaSet_UserTV_Show(Id, SeasonId)
values(8, 2);

insert into MediaLib.dbo.UserMediaSet_UserTV_Show(Id, SeasonId)
values(9, 2);

/***************** Actors *************************************/

--1
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Arnold', 'Schwarzenegger');

--2
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Michael', 'Biehn');

--3
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Linda', 'Hamilton');

--4
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Marilyn', 'Burns');

--5
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Edwin', 'Neal');

--6
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Josh', 'Pais');

--7
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Michelan', 'Sisi');

--8
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Leif', 'Tilden');

--9
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('David', 'Forman');

--10
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Emilio', 'Estevez');

--11
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Pat', 'Hingle');

--12
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Jack', 'Nicholson');

--13
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Shelley', 'Duvall');

--14
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Trey', 'Parker');

--15
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Matt', 'Stone');

--16
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Josh', 'Radnor');

--17
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Jason', 'Segel');

--18
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Cobie', 'Smulders');

--19
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Neil Patrick', 'Harris');

--20
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Alyson', 'Hannigan');

--21
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Johann Wolfgang', 'Goethe');

--22
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('John Ronald Reuel', 'Tolkien');

--23
insert into MediaLib.dbo.PersonSet (FirstName, LastName)
values ('Stephen', 'King');

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (1);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (2);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (3);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (4);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (5);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (6);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (7);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (8);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (9);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (10);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (11);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (12);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (13);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (14);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (15);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (16);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (17);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (18);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (19);

insert into MediaLib.dbo.PersonSet_Actor(Id)
values (20);

insert into MediaLib.dbo.PersonSet_Author(Id)
values (21);

insert into MediaLib.dbo.PersonSet_Author(Id)
values (22);

insert into MediaLib.dbo.PersonSet_Author(Id)
values (23);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(1,1);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(2,1);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(3,1);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(4,2);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(5,2);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(6,3);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(7,3);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(8,3);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(9,3);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(10,4);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(11,4);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(12,5);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(13,5);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(14,6);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(15,6);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(16,8);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(17,8);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(18,8);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(19,8);

insert into MediaLib.dbo.ActorVideo(Actor_Id, Video_Id)
values(20,8);

insert into MediaLib.dbo.AuthorBook(Author_Id, Book_Id)
values(21,7);

insert into MediaLib.dbo.AuthorBook(Author_Id, Book_Id)
values(22,10);

insert into MediaLib.dbo.AuthorBook(Author_Id, Book_Id)
values(23,9);

-- MediaRequests

-- Movies for MediaRequests
--13
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('MovieRequest1', 'MovieRequest1', 'Action', 0, 0, 0, 'true', '2011-04-02');

--14
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('MovieRequest2', 'MovieRequest2', 'Action', 0, 0, 0, 'true', '2011-04-02');

--15
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('MovieRequest3', 'MovieRequest3', 'Action', 0, 0, 0, 'true', '2011-04-02');

--16
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('MovieRequest4', 'MovieRequest4', 'Action', 0, 0, 0, 'true', '2011-04-02');

--17
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('MovieRequest5', 'MovieRequest5', 'Action', 0, 0, 0, 'true', '2011-04-02');

--18
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('MovieRequest6', 'MovieRequest6', 'Action', 0, 0, 0, 'true', '2011-04-02');

--19
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('MovieRequest7', 'MovieRequest7', 'Action', 0, 0, 0, 'true', '2011-04-02');

--20
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('MovieRequest8', 'MovieRequest8', 'Action', 0, 0, 0, 'true', '2011-04-02');

--21
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('MovieRequest9', 'MovieRequest9', 'Action', 0, 0, 0, 'true', '2011-04-02');

--22
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('MovieRequest10', 'MovieRequest10', 'Action', 0, 0, 0, 'true', '2011-04-02');

-- Books for MediaRequests
--23
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('BookRequest1', 'BookRequest1', 'Action', 0, 0, 0, 'true', '2011-04-02');

--24
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('BookRequest2', 'BookRequest2', 'Action', 0, 0, 0, 'true', '2011-04-02');

--25
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('BookRequest3', 'BookRequest3', 'Action', 0, 0, 0, 'true', '2011-04-02');

--26
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('BookRequest4', 'BookRequest4', 'Action', 0, 0, 0, 'true', '2011-04-02');

--27
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('BookRequest5', 'BookRequest5', 'Action', 0, 0, 0, 'true', '2011-04-02');

--28
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('BookRequest6', 'BookRequest6', 'Action', 0, 0, 0, 'true', '2011-04-02');

--29
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('BookRequest7', 'BookRequest7', 'Action', 0, 0, 0, 'true', '2011-04-02');

--30
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('BookRequest8', 'BookRequest8', 'Action', 0, 0, 0, 'true', '2011-04-02');

--31
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('BookRequest9', 'BookRequest9', 'Action', 0, 0, 0, 'true', '2011-04-02');

--32
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('BookRequest10', 'BookRequest10', 'Action', 0, 0, 0, 'true', '2011-04-02');

-- TVShows for MediaRequests
--33
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('TVShowRequest1', 'TVShowRequest1', 'Action', 0, 0, 0, 'true', '2011-04-02');

--34
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('TVShowRequest2', 'TVShowRequest2', 'Action', 0, 0, 0, 'true', '2011-04-02');

--35
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('TVShowRequest3', 'TVShowRequest3', 'Action', 0, 0, 0, 'true', '2011-04-02');

--36
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('TVShowRequest4', 'TVShowRequest4', 'Action', 0, 0, 0, 'true', '2011-04-02');

--37
insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, TotalRaters, AverageRating, Pending, AddingDate)
values ('TVShowRequest5', 'TVShowRequest5', 'Action', 0, 0, 0, 'true', '2011-04-02');


insert into MediaLib.dbo.MediaSet_Video (Id)
values(13);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(14);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(15);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(16);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(17);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(18);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(19);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(20);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(21);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(22);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(33);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(34);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(35);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(36);

insert into MediaLib.dbo.MediaSet_Video (Id)
values(37);

 
insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(13, '1984-10-26');

insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(14, '1974-10-01');

insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(15, '1990-03-30');

insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(16, '1986-07-25');

insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(17, '1980-05-23');

insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(18, '1984-09-07');

insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(19, '1997-08-13');

insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(20, '2005-09-19');

insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(21, '1997-08-13');

insert into MediaLib.dbo.MediaSet_Movie (Id, ReleaseDate)
values(22, '2005-09-19');


insert into MediaLib.dbo.MediaSet_TV_Show (Id, ShowBeginning)
values(33, '2005-09-19');

insert into MediaLib.dbo.MediaSet_TV_Show (Id, ShowBeginning)
values(34, '2005-09-19');

insert into MediaLib.dbo.MediaSet_TV_Show (Id, ShowBeginning)
values(35, '2005-09-19');

insert into MediaLib.dbo.MediaSet_TV_Show (Id, ShowBeginning)
values(36, '2005-09-19');

insert into MediaLib.dbo.MediaSet_TV_Show (Id, ShowBeginning)
values(37, '2005-09-19');


insert into MediaLib.dbo.SeasonSet (Number, TV_ShowId)
values(1, 33);

insert into MediaLib.dbo.SeasonSet (Number, TV_ShowId)
values(1, 34);

insert into MediaLib.dbo.SeasonSet (Number, TV_ShowId)
values(1, 35);

insert into MediaLib.dbo.SeasonSet (Number, TV_ShowId)
values(1, 36);

insert into MediaLib.dbo.SeasonSet (Number, TV_ShowId)
values(1, 37);


insert into MediaLib.dbo.MediaSet_Book (Id, Isbn)
values(23, '0123456789');

insert into MediaLib.dbo.MediaSet_Book (Id)
values(24);

insert into MediaLib.dbo.MediaSet_Book (Id, Isbn)
values(25, '0123456789');

insert into MediaLib.dbo.MediaSet_Book (Id)
values(26);

insert into MediaLib.dbo.MediaSet_Book (Id, Isbn)
values(27, '0123456789');

insert into MediaLib.dbo.MediaSet_Book (Id)
values(28);

insert into MediaLib.dbo.MediaSet_Book (Id, Isbn)
values(29, '0123456789');

insert into MediaLib.dbo.MediaSet_Book (Id)
values(30);

insert into MediaLib.dbo.MediaSet_Book (Id, Isbn)
values(31, '0123456789');

insert into MediaLib.dbo.MediaSet_Book (Id)
values(32);

--InsertRequests

--Movies
insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 13, '2011-06-20 00:00:01');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 14, '2011-06-20 00:00:04');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 15, '2011-06-20 00:00:07');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 16, '2011-06-20 00:00:10');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 17, '2011-06-20 00:00:13');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 18, '2011-06-20 00:00:16');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 19, '2011-06-20 00:00:19');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 20, '2011-06-20 00:00:22');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 21, '2011-06-20 00:00:25');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 22, '2011-06-20 00:00:28');

--Books
insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 23, '2011-06-20 00:00:02');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 24, '2011-06-20 00:00:05');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 25, '2011-06-20 00:00:08');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 26, '2011-06-20 00:00:11');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 27, '2011-06-20 00:00:14');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 28, '2011-06-20 00:00:17');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 29, '2011-06-20 00:00:20');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 30, '2011-06-20 00:00:23');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 31, '2011-06-20 00:00:26');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 32, '2011-06-20 00:00:29');

--TVShows
insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 33, '2011-06-20 00:00:03');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 34, '2011-06-20 00:00:06');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 35, '2011-06-20 00:00:09');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 36, '2011-06-20 00:00:12');

insert into MediaLib.dbo.InsertRequestSet(UserId, Media_Id, RequestDate)
values(1, 37, '2011-06-20 00:00:15');