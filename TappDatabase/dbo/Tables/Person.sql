CREATE TABLE [dbo].[Person] (
    [person_id] INT           IDENTITY (1, 1) NOT NULL,
    [username]  VARCHAR (20)  NOT NULL,
    [email]     VARCHAR (20)  NOT NULL,
    [languages] VARCHAR (100) NULL,
    CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED ([person_id] ASC)
);

