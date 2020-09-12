CREATE TABLE [dbo].[cliente]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [nome] NVARCHAR(30) NOT NULL, 
    [cpf] NCHAR(14) NOT NULL, 
    [data_nascimento] DATETIME NOT NULL, 
    [idade] INT NULL
)
