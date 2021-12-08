
-- Drop tables section ------------------------------------------------

If OBJECT_ID(N'Project', N'U') is not null drop table [Project]
If OBJECT_ID(N'Person', N'U') is not null drop table [Person]

-- Create tables section ------------------------------------------------

-- Table Person

CREATE TABLE [Person] (
  [person_id] int NOT NULL IDENTITY(1,1),
  [username] varchar(20) NOT NULL UNIQUE,
  [email] varchar(20) NOT NULL,
  [is_active] bit default 1,
  [languages] varchar(100),

  CONSTRAINT [PK_person] PRIMARY KEY ([person_id])
);
go

-- Table Project

CREATE TABLE [Project] (
  [project_id] int NOT NULL IDENTITY(1,1),
  [name] varchar(20) NOT NULL,
  [original_language] varchar(10) NOT NULL,
  [translate_language] varchar(10) NOT NULL,
  [original_file] varchar(500) NOT NULL,
  [translate_file] varchar(500),

  [requester_id] int,
  [translator_id] int,
  [is_completed] bit default 0,


  CONSTRAINT [PK_project] PRIMARY KEY ([project_id]),
  CONSTRAINT [FK_project_requester] FOREIGN KEY ([requester_id]) REFERENCES [Person]([person_id]),
  CONSTRAINT [FK_project_translator] FOREIGN KEY ([translator_id]) REFERENCES [Person]([person_id]) 
);
go

-- Delete section ------------------------------------------------

DElETE FROM Person

DELETE FROM Project
where project_id > 1000

-- Insert section ------------------------------------------------

INSERT INTO [Person] ([username], [email], [languages])
VALUES
	('User1', 'u1@email.cz', 'czech'),
	('User2', 'u2@email.cz', 'english, czech'),
	('User3', 'u3@email.cz', 'chinese, english'),
	('User4', 'u4@email.cz', 'english,german,chinese,czech');

--1
INSERT INTO [Project] ([name], [original_language], [translate_language], [original_file], [translate_file], [requester_id], [translator_id])
VALUES	('Poem', 'english', 'czech', 'I have been astonished that men could die martyrs
for their religion--
I have shuddered at it,
I shudder no more.
I could be martyred for my religion.
Love is my religion
and I could die for that.
I could die for you.
My Creed is Love and you are its only tenet.',NULL,1,NULL);

--2
INSERT INTO [Project] ([name], [original_language], [translate_language], [original_file], [translate_file], [requester_id], [translator_id])
VALUES ('Pohádka', 'czech', 'english', 'Bylo nebylo, slunce svítilo. A za devatero horami a devatero řekami?', 'Once upon a time...',1, 2);

--3
INSERT INTO [Project] ([name], [original_language], [translate_language], [original_file], [translate_file], [requester_id], [translator_id])
VALUES ('Short text', 'english', 'chinese', 'Do you think you can translate this?
I do not think so. BUt you can try.',
'Ni juede ni hui fanyi zhege juci? Wo juede ni bu hui. Danshi ni keyi shi shi.',1, 3);

--4
INSERT INTO [Project] ([name], [original_language], [translate_language], [original_file], [translate_file], [requester_id], [translator_id])
VALUES ('Nihao Zhongguo', 'chinese', 'german', 'Wo buzhidao ni xianzai ganshenme! Wo hen haipa!',NULL,3, NULL);

--5
INSERT INTO [Project] ([name], [original_language], [translate_language], [original_file], [translate_file], [requester_id], [translator_id])
VALUES ('Wir stehen', 'german', 'english', 'Gutan Tag! Ich bin ein Berliner!',NULL,3, NULL);

--6
INSERT INTO [Project] ([name], [original_language], [translate_language], [original_file], [translate_file], [requester_id], [translator_id])
VALUES ('Suggestion', 'english', 'german', 'Try to use different user, mate.',NULL,2, 4);

-- Select section ------------------------------------------------

SELECT *
FROM Project

SELECT *
FROM Person