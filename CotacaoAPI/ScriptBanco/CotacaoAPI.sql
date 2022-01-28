
CREATE DATABASE CotacaoAPI

USE CotacaoAPI

CREATE TABLE Cotacao (
	IdCotacao UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
	CNPJComprador NVARCHAR (25) NOT NULL,
	CNPJFornecedor NVARCHAR (25) NOT NULL,
	NumeroCotacao INT NOT NULL,
	DataCotacao DATETIME NOT NULL,
	DataEntregaCotacao DATETIME NOT NULL,
	CEP NVARCHAR (10) NOT NULL,
	Logradouro NVARCHAR (50),
	Numero NVARCHAR(10),
	Complemento NVARCHAR (20),
	Cidade NVARCHAR(50),
	Bairro NVARCHAR (50),
	UF NVARCHAR(2),
	Observacao NVARCHAR (100)
);

CREATE TABLE CotacaoItem (
    IdCotacaoItem UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
	Descricao NVARCHAR(100) NOT NULL,
	NumeroItem INT NOT NULL,
	Preco DECIMAL (18,2),
	Quantidade INT NOT NULL,
	Marca NVARCHAR (30),
	Unidade NVARCHAR (10),
	IdCotacao UNIQUEIDENTIFIER

	CONSTRAINT fk_CotacaoItem_Cotacao FOREIGN KEY (IdCotacao) REFERENCES Cotacao (IdCotacao)
);
