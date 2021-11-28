-- Table Person

CREATE TABLE [Person] (
  [person_id] int NOT NULL IDENTITY(1,1),
  [username] varchar(20) NOT NULL,
  [email] varchar(20) NOT NULL,

  CONSTRAINT [PK_person] PRIMARY KEY ([person_id])
);