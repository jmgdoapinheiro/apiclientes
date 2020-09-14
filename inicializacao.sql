CREATE TABLE cliente (
    `id`    BIGINT        AUTO_INCREMENT  NOT NULL,
    `nome`  NVARCHAR (30) NOT NULL,
    `cpf`   NCHAR (14)    NOT NULL,
    `idade` INT           NULL,
    PRIMARY KEY (`id` ASC),
    UNIQUE (`cpf` ASC)
);


CREATE TABLE endereco
(
	`id_cliente` BIGINT NOT NULL PRIMARY KEY, 
    `logradouro` NVARCHAR(50) NOT NULL, 
    `bairro` NVARCHAR(40) NOT NULL, 
    `cidade` NVARCHAR(40) NOT NULL, 
    `estado` NVARCHAR(40) NOT NULL, 
    CONSTRAINT `FK_endereco_cliente` FOREIGN KEY (`id_cliente`) REFERENCES cliente(`id`) ON DELETE CASCADE
);