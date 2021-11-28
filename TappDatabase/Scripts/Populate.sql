


INSERT INTO [Person] ([username], [email])
VALUES
	('User1', 'u1'),
	('User2', 'u2'),
	('User3', 'u3');
GO

INSERT INTO [Project] ([name], [original_language], [translate_language], [original_file], [translate_file], [requester_id])
VALUES	('Poem', 'english', 'czech', 'Shows - Rows','', 1),
		('Pohadka', 'czech', 'english', 'Bylo nebylo', 'Once upon a time', 1),
		('Nihao', 'chinese', 'english', 'wo shi', 'My name is', 1);
GO

Select *
from Project