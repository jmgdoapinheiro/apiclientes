CREATE TABLE [dbo].[endereco] (
    [id_cliente] BIGINT     NOT NULL,
    [logradouro] NCHAR (50) NOT NULL,
    [bairro]     NCHAR (40) NOT NULL,
    [cidade]     NCHAR (40) NOT NULL,
    [estado]     NCHAR (40) NOT NULL,
    PRIMARY KEY CLUSTERED ([id_cliente] ASC),
    CONSTRAINT [FK_endereco_cliente] FOREIGN KEY ([id_cliente]) REFERENCES [dbo].[cliente] ([id]) ON DELETE CASCADE
);

