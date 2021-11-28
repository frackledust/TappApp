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


  CONSTRAINT [PK_project] PRIMARY KEY ([project_id]),
  CONSTRAINT [FK_project_requester] FOREIGN KEY ([requester_id]) REFERENCES [Person]([person_id]),
  CONSTRAINT [FK_project_translator] FOREIGN KEY ([translator_id]) REFERENCES [Person]([person_id]) 
);