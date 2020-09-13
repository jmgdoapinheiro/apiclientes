CREATE TABLE [dbo].[cliente] (
    [id]    BIGINT        IDENTITY (1, 1) NOT NULL,
    [nome]  NVARCHAR (30) NOT NULL,
    [cpf]   NCHAR (14)    NOT NULL,
    [idade] INT           NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([cpf] ASC)
);