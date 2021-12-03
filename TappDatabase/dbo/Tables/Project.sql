CREATE TABLE [dbo].[Project] (
    [project_id]         INT           IDENTITY (1, 1) NOT NULL,
    [name]               VARCHAR (20)  NOT NULL,
    [original_language]  VARCHAR (10)  NOT NULL,
    [translate_language] VARCHAR (10)  NOT NULL,
    [original_file]      VARCHAR (500) NOT NULL,
    [translate_file]     VARCHAR (500) NULL,
    [requester_id]       INT           NULL,
    [translator_id]      INT           NULL,
    CONSTRAINT [PK_project] PRIMARY KEY CLUSTERED ([project_id] ASC),
    CONSTRAINT [FK_project_requester] FOREIGN KEY ([requester_id]) REFERENCES [dbo].[Person] ([person_id]),
    CONSTRAINT [FK_project_translator] FOREIGN KEY ([translator_id]) REFERENCES [dbo].[Person] ([person_id])
);

