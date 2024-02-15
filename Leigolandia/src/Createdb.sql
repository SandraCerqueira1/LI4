IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'Leigolandia')
BEGIN
    CREATE DATABASE Leigolandia;
END
GO

USE Leigolandia;
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Utilizador')
BEGIN
   CREATE TABLE Utilizadores
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(MAX) NOT NULL,
    Username VARCHAR(40)  NOT NULL UNIQUE,
    Password NVARCHAR(MAX) NOT NULL,
    DataDeNascimento DATETIME2 NOT NULL,
    Telemovel INT NOT NULL,
    Email NVARCHAR(MAX) NOT NULL,
    Nif INT NOT NULL,
    Morada NVARCHAR(MAX) NOT NULL,
    IsAdministrador BIT NOT NULL
);

END
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Categorias')
BEGIN
CREATE TABLE Categorias
   (
       Id INT PRIMARY KEY IDENTITY(1,1),
       Nome NVARCHAR(MAX) NOT NULL
   );

END
GO







IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Lego')
BEGIN
CREATE TABLE Lego
(
       Id INT PRIMARY KEY IDENTITY(1,1),
       Nome NVARCHAR(MAX) NOT NULL,
       SerialNumber INT NOT NULL,
       Estado NVARCHAR(MAX) NOT NULL,
       AnoDeLancamento INT NOT NULL,
       NumeroDePecas INT NOT NULL,
       IdadeRecomendada INT NOT NULL,
       Imagem NVARCHAR(MAX) NOT NULL,
       Descricao NVARCHAR(MAX) NOT NULL,
       NumeroDeMiniFiguras INT NOT NULL,
       Certificacao NVARCHAR(MAX),
       Dimensoes NVARCHAR(MAX),
       CategoriaId INT ,
       CONSTRAINT FK_Legos_Categorias FOREIGN KEY (CategoriaId)
       REFERENCES Categorias(Id)
       ON DELETE CASCADE
   );
END
Go





IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Estatisticas')
BEGIN
   CREATE TABLE Estatisticas
   (
       Id INT PRIMARY KEY IDENTITY(1,1),
       LucroTotal FLOAT NOT NULL,
       LucroMedio FLOAT NOT NULL,
       NumeroDeAvaliacoes INT NOT NULL,
       AvaliacaoMedia FLOAT NOT NULL,
       LeiloesCriados INT NOT NULL,
       LeiloesParticipados INT NOT NULL,
       LeiloesGanhos INT NOT NULL,
       GastoTotal FLOAT NOT NULL,
       GastoMedio FLOAT NOT NULL,
       UtilizadorId INT NOT NULL,
       CONSTRAINT FK_Estatisticas_Utilizadores FOREIGN KEY (UtilizadorId)
       REFERENCES Utilizadores(Id)
       ON DELETE CASCADE
   );

END
Go

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HistoricoCompras')
BEGIN
   CREATE TABLE HistoricoCompras
   (
       Id INT PRIMARY KEY IDENTITY(1,1),
       Titulo NVARCHAR(MAX) NOT NULL,
       PrecoFinal FLOAT NOT NULL,
       LegoId INT NOT NULL,
       UtilizadorId INT NOT NULL,
       CONSTRAINT FK_HistoricoCompras_Legos FOREIGN KEY (LegoId)
       REFERENCES Lego(Id)
       ON DELETE CASCADE,
       CONSTRAINT FK_HistoricoCompras_Utilizadores FOREIGN KEY (UtilizadorId)
       REFERENCES Utilizadores(Id)
       ON DELETE CASCADE
   );
END
Go

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HistoricoVendas')
BEGIN
   CREATE TABLE HistoricoVendas
   (
       Id INT PRIMARY KEY IDENTITY(1,1),
       Titulo NVARCHAR(MAX) NOT NULL,
       PrecoFinal FLOAT NOT NULL,
       LegoId INT NOT NULL,
       UtilizadorId INT NOT NULL,
       CONSTRAINT FK_HistoricoVendas_Legos FOREIGN KEY (LegoId)
       REFERENCES Lego(Id)
       ON DELETE CASCADE,
       CONSTRAINT FK_HistoricoVendas_Utilizadores FOREIGN KEY (UtilizadorId)
       REFERENCES Utilizadores(Id)
       ON DELETE CASCADE
   );

END
Go

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Leiloes')
BEGIN
   CREATE TABLE Leiloes
   (
       Id INT PRIMARY KEY IDENTITY(1,1),
       Titulo NVARCHAR(MAX) NOT NULL,
	   DataInicio DATETIME2 NOT NULL,
       DataFiM DATETIME2 NOT NULL,
       PrecoBase FLOAT NOT NULL,
       PrecoAtual FLOAT NOT NULL,
       Descricao NVARCHAR(MAX) NOT NULL,
       LegoId INT NOT NULL,
	   Estado VARCHAR(40) NOT NULL,
       VendedorId INT NOT NULL,
       CONSTRAINT FK_Leiloes_Legos FOREIGN KEY (LegoId)
       REFERENCES Lego(Id)
       ON DELETE CASCADE,
       CONSTRAINT FK_Leiloes_Utilizadores FOREIGN KEY (VendedorId)
       REFERENCES Utilizadores(Id)
       ON DELETE CASCADE
   );

END
Go



IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Licitacoes')
BEGIN
   CREATE TABLE Licitacoes
   (
       Id INT PRIMARY KEY IDENTITY(1,1),
       Valor FLOAT NOT NULL,
       IdLicitador INT NOT NULL,
       IdLeilao INT NOT NULL,
       CONSTRAINT FK_Licitacoes_Leiloes FOREIGN KEY (IdLeilao)
       REFERENCES Leiloes(Id)
       ON DELETE CASCADE,
       CONSTRAINT FK_Licitacoes_Utilizadores FOREIGN KEY (IdLicitador)
       REFERENCES Utilizadores(Id)
       ON DELETE NO ACTION
   );

   
END
GO

USE Leigolandia;
GO

-----------------Povoamento---------------------------------
-- Inserção de dados na tabela 'Utilizadores'
INSERT INTO Utilizadores (Nome, Username, Password, DataDeNascimento, Telemovel, Email, Nif, Morada, IsAdministrador)
VALUES 
    ('João Silva', 'joao123', '123', '1990-01-15', 912345678, 'joao@email.com', 123456789, 'Rua A, Lisboa', 0),
    ('Admin', 'SenhorAlberto', '456', '1985-05-20', 987654321, 'admin@email.com', 987654321, 'Rua B, Porto', 1),
	('Sandra Cerqueira', 'sandracerqueira','789', '2003-09-24', 937564309,'sandra@email.com',342678478,'Rua 1, Braga', 0),
	('Diogo Miranda', 'dgmiranda','476', '1997-09-24', 937564379,'diogo@email.com',342984378,'Rua mangoeiro, Braga', 0);


-- Inserção de dados na tabela 'Categorias'
INSERT INTO Categorias (Nome)
VALUES 
    ('City'),
    ('Technic'),
	('MARVEL'),
    ('Star_Wars'),
	('Outras');


-- Inserção de dados na tabela 'Lego'
-- Povoamento da tabela Lego
INSERT INTO Lego (Nome, SerialNumber, Estado, AnoDeLancamento, NumeroDePecas, IdadeRecomendada, Imagem, Descricao, NumeroDeMiniFiguras, Certificacao, Dimensoes, CategoriaId)
VALUES 
('Football Stadium', 67890, 'Usado', 2021, 1000, 12, '/Media/uploads/prod2.png', 'Lego Football Stadium em boas condições', 10, 'Certificado ABC', '20x15x8', 1),
('Comboio do Oriente', 21344, 'Usado', 2022, 2540, 18, '/Media/uploads/prod4.png', 'Comboio do Oriente em boas condições', 8, 'Certificado DEF', '12x8x116', 1),
('Navio Explorador do Ártico', 60368, 'Novo', 2022, 815, 7, '/Media/uploads/prod5.png', 'Navio Explorador do Ártico em boas condições', 8, 'Certificado DEF', '12x8x116', 1),
('Comboio de Carga', 60198, 'Novo', 2022, 1226, 6, '/Media/uploads/prod10.png', 'Comboio de carga novo', 8, 'Certificado DEF', '12x8x116', 1),
('Technic Car', 12345, 'Novo', 2022, 500, 10, '/Media/uploads/prod1.png', 'Lego Technic Car em boas condições', 5, 'Certificado XYZ', '10x10x5', 2),
('Carro de Corrida McLaren Fórmula 1', 42141, 'Novo', 2022, 1434, 18, '/Media/uploads/prod11.png', 'Para os maiores fãs de F1', 5, 'Certificado XYZ', '10x10x5', 2),
('Porsche 911 RSR', 42096, 'Novo', 2022, 1580, 10, '/Media/uploads/prod12.png', 'Para quem gosta de porshe', 5, 'Certificado XYZ', '10x10x5', 2),
('Audi RS Q e-tron', 42160, 'Novo', 2022, 1275, 10, '/Media/uploads/prod13.png', 'Para os maiores fãs da audi', 5, 'Certificado XYZ', '10x10x5', 2),
('Torre dos Vingadores', 76269, 'Novo', 2022, 5201, 18, '/Media/uploads/prod6.png', 'Torre dos Vingadores nova', 31, 'Certificado DEF', '12x8x116', 3),
('Endgame A Batalha Final', 76266, 'Novo', 2022, 787, 10, '/Media/uploads/prod7.png', 'Endgame a batalha final em boas condições', 31, 'Certificado DEF', '36x14x10', 3),
('Escudo Capitão America', 76262, 'Novo', 2022, 3128, 18, '/Media/uploads/prod8.png', 'Escudo Capitão America em boas condições', 0, 'Certificado DEF', '12x8x116', 3),
('Hulkbuster', 76210, 'Novo', 2022, 4125, 18, '/Media/uploads/prod9.png', 'Valioso Hulkbuster', 0, 'Certificado DEF', '21x19x10', 3),
('Baby Yoda', 75318, 'Novo', 2022, 1075, 10, '/Media/uploads/prod14.png', 'Precious cargo lego', 1, 'Certificado DEF', '21x19x10', 4),
('Millennium Falcon', 75257, 'Usado', 2022, 1353, 9, '/Media/uploads/prod15.png', 'Exemplar em ótimo estado', 7, 'Certificado DEF', '21x19x10', 4),
('Ghost e Phantom II', 75357, 'Novo', 2022, 1394, 10, '/Media/uploads/prod16.png', 'Um ótimo acrescento a qualquer coleção', 5, 'Certificado DEF', '21x19x10', 4),
('Walker AT-TE', 75337, 'Novo', 2022, 1082, 10, '/Media/uploads/prod17.png', 'Um ótimo acrescento a qualquer coleção', 9, 'Certificado DEF', '21x19x10', 4),
('Jungle Adventure', 13579, 'Novo', 2022, 800, 8, '/Media/uploads/prod3.png', 'Lego Jungle Adventure em boas condições', 8, 'Certificado DEF', '15x12x6', 5),
('Pantera Negra', 13579, 'Novo', 2022, 800, 8, '/Media/uploads/prod18.png', 'Lego em boas condições', 0, 'Certificado DEF', '15x12x6', 3),
('Baixa', 13579, 'Novo', 2022, 800, 8, '/Media/uploads/prod19.png', 'Lego em boas condições', 0, 'Certificado DEF', '15x12x6', 1);

GO


-- Inserção de dados na tabela 'Leiloes'
INSERT INTO Leiloes (Titulo, DataInicio, DataFiM, PrecoBase, PrecoAtual, Descricao, LegoId, Estado, VendedorId)
VALUES
('Lego Footbal Stadium','2024-01-24 20:00:00', '2024-01-31 22:00:00', 400.0,400.0, 'Leilão grande estadio',1, 'Decorrer', 3),
('Comboio do Oriente','2022-01-24 16:00:00', '2022-01-24 18:00:00', 400.0,800.0, 'Leilão do Lego em fantastico estado',2, 'Terminado', 1),
('Navio Explorador do Ártico','2024-01-24 16:00:00', '2024-01-31 18:00:00', 300.0,300.0, 'Leilão do Lego em fantastico estado',3, 'Terminado', 3),
('Comboio de Carga','2024-01-26 16:00:00', '2024-01-31 18:00:00', 700.0, 700.0, 'Leilão do Lego Comboio de Carga', 4, 'Decorrer', 4),
('Lego Technic Car','2024-01-24 17:00:00', '2024-01-31 12:00:00', 230.0,230.0, 'Leilão do Lego Technic car exclusive',5, 'Decorrer', 1),
('Leilão Carro de Corrida McLaren F1','2024-01-27 14:00:00', '2024-01-31 16:00:00', 800.0, 800.0, 'Leilão do Lego Carro de Corrida McLaren F1', 6, 'Decorrer', 2),
('Leilão Porsche 911 RSR','2024-01-27 16:00:00', '2024-01-31 18:00:00', 900.0, 900.0, 'Leilão do Lego Porsche 911 RSR', 7, 'Decorrer', 3),
('Leilão Audi RS Q e-tron','2024-01-27 18:00:00', '2024-01-31 20:00:00', 100.0, 160.0, 'Leilão do Lego Audi RS Q e-tron', 8, 'Terminado', 2), 
('Torre dos Vingadores','2024-01-24 16:00:00', '2024-01-31 18:00:00', 200.0,200.0, 'Leilão do Lego em fantastico estado',9, 'Decorrer', 2),
('Endgame A Batalha Final','2024-01-24 16:00:00', '2024-01-31 18:00:00', 280.0,280.0, 'Leilão do Lego em fantastico estado',10, 'Decorrer', 2),
('Escudo Capitão America','2024-01-28 18:00:00', '2024-01-31 20:00:00', 100.0, 100.0, 'Leilão do Lego Escudo Capitão America', 11, 'Decorrer', 1),
('Hulkbuster','2024-01-28 20:00:00', '2024-01-31 22:00:00', 360.0, 360.0, 'Leilão do Lego Hulkbuster', 12, 'Terminado', 4), 
('Baby Yoda','2024-01-29 14:00:00', '2024-01-31 16:00:00', 90.0, 90.0, 'Leilão do Lego Baby Yoda', 13, 'Decorrer', 1),
('Millennium Falcon','2024-01-29 16:00:00', '2024-01-31 18:00:00', 200.0,200.0, 'Leilão do Lego Millennium Falcon', 14, 'Decorrer', 2),
('Ghost e Phantom II','2024-01-29 18:00:00', '2024-01-31 20:00:00', 100.0, 100.0, 'Leilão do Lego Ghost e Phantom II', 15, 'Decorrer', 1),
('Leilão Walker AT-TE','2024-01-29 20:00:00', '2024-01-31 22:00:00', 180.0, 180.0, 'Leilão do Lego Walker AT-TE', 16, 'Decorrer', 4),
('Jungle Adventure','2024-01-24 16:00:00', '2024-01-31 18:00:00', 300.0,300.0, 'Leilão do Lego em fantastico estado',17, 'Decorrer', 3),
('Pantera Negra','2024-01-24 16:00:00', '2024-01-31 18:00:00', 300.0,300.0, 'Leilão do Lego em fantastico estado',18, 'Decorrer', 2),
('Baixa','2024-01-24 16:00:00', '2024-01-31 18:00:00', 300.0,300.0, 'Leilão do Lego em fantastico estado',19, 'Decorrer', 2);
GO

-- Inserção de dados na tabela 'Licitacoes'
INSERT INTO Licitacoes (Valor, IdLicitador, IdLeilao)
VALUES 
    (450.0, 3, 2),
    (550.0, 4, 2),
	(750.0, 3, 2),
	(800.0, 4, 2),
	(160.0, 3, 8),
	(300.0, 1, 3),
	(360.0, 2, 12);

-- Inserção de dados na tabela 'HistoricoCompras'
INSERT INTO HistoricoCompras (Titulo, PrecoFinal, LegoId, UtilizadorId)
VALUES 
    ('Leilão do Comboio do Oriente', 800.00, 2, 4);

-- Povoamento da tabela HistoricoVendas
INSERT INTO HistoricoVendas (Titulo, PrecoFinal, LegoId, UtilizadorId)
VALUES 
	('Leilão do Comboio do Oriente', 800.00, 2, 1);

-- Povoamento da tabela estatisticas
INSERT INTO Estatisticas(LucroTotal,LucroMedio,NumeroDeAvaliacoes,AvaliacaoMedia, LeiloesCriados,LeiloesParticipados,LeiloesGanhos,GastoTotal,GastoMedio, UtilizadorId)
VALUES 
	(800.0,800.0,0,0,1,0,0,0,0,1),
	(0,0,0,0,0,1,1,800.0,800.0,4)
;


------------------checkpovoamento-------------------------------------------------
SELECT * FROM Utilizadores;
SELECT * FROM Categorias;
SELECT * FROM Estatisticas;
SELECT * FROM HistoricoCompras;
SELECT * FROM HistoricoVendas;
SELECT * FROM Lego;
SELECT * FROM Leiloes;
SELECT * FROM Licitacoes;


DELETE FROM Utilizadores;
DELETE FROM Categorias;
--Método para apagar a base de dados-------------------------------
USE master;
GO
ALTER DATABASE Leigolandia SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

USE master;
GO
DROP DATABASE Leigolandia;
GO
---------------------------------
