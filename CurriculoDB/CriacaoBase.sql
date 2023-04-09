DROP DATABASE IF EXISTS CurriculoDB
GO

CREATE DATABASE CurriculoDB
GO

USE CurriculoDB
GO


-- Apagando as tabelas caso existam
BEGIN
	DROP TABLE IF EXISTS [dbo].[tb_Idioma]
	DROP TABLE IF EXISTS [dbo].[tb_Formacao]
	DROP TABLE IF EXISTS [dbo].[tb_Experiencia]
	DROP TABLE IF EXISTS [dbo].[tb_Curriculo]
	DROP TABLE IF EXISTS [dbo].[tb_Area]
END
GO

-- Cria as tabelas
BEGIN

CREATE TABLE [dbo].[tb_Area]
(
	 [Id_Area]		INT NOT NULL PRIMARY KEY IDENTITY(1,1)
	,[Desc_Area]	VARCHAR(50) NOT NULL,
)

CREATE TABLE [dbo].[tb_Curriculo]
(
	 [Id_Curriculo]		INT NOT NULL PRIMARY KEY IDENTITY(1,1)
	,[Id_Area]			INT NOT NULL FOREIGN KEY REFERENCES [dbo].[tb_Area](Id_Area)
	,Nome				VARCHAR(50)		NOT NULL
	,CPF				VARCHAR(14)		NOT NULL
	,Endereco			VARCHAR(200)
	,Telefone			VARCHAR(15)
	,Email				VARCHAR(50)
	,Cargo_Pretendido	VARCHAR(50)		NOT NULL
	
	,Id_Icone			INT
	,Pretensao_Salarial	DECIMAL(10,2)
)

CREATE TABLE [dbo].[tb_Experiencia]
(
	 [Id_Experiencia]	INT				NOT NULL PRIMARY KEY IDENTITY(1,1)
	,[Id_Curriculo]		INT				NOT NULL FOREIGN KEY REFERENCES [dbo].[tb_Curriculo](Id_Curriculo)
	,[Desc_Experiencia]	VARCHAR(200)	NOT NULL
)

CREATE TABLE [dbo].[tb_Formacao]
(
	 [Id_Formacao]		INT			NOT NULL PRIMARY KEY IDENTITY(1,1)
	,[Id_Curriculo]		INT			NOT NULL FOREIGN KEY REFERENCES [dbo].[tb_Curriculo](Id_Curriculo)
	,[Desc_Formacao]	VARCHAR(200)	NOT NULL
)

CREATE TABLE [dbo].[tb_Idioma]
(
	 [Id_Idioma]			INT				NOT NULL PRIMARY KEY IDENTITY(1,1)
	,[Id_Curriculo]			INT				NOT NULL FOREIGN KEY REFERENCES [dbo].[tb_Curriculo](Id_Curriculo)
	,[Desc_Idioma]			VARCHAR(200)	NOT NULL
	,[Grau_Proficiencia]	INT	NOT NULL
)

END
GO


-- Insere valores para teste
BEGIN

	INSERT INTO [dbo].[tb_Area] ([Desc_Area]) 
	VALUES
		('Ciências Agrárias'),
		('Ciências Biológicas'),
		('Ciências da Saúde'),
		('Ciências Exatas e da Terra'),
		('Engenharias'),
		('Ciência e Tecnologia'),
		('Ciências Humanas'),
		('Ciências Sociais Aplicadas'),
		('Linguística, Letras e Artes')

	INSERT INTO [dbo].[tb_Curriculo] ([Id_Area],[Nome],[CPF],[Endereco],[Telefone],[Email],[Cargo_Pretendido],[Id_Icone],[Pretensao_Salarial]) 
	VALUES
		 (2,'Atila Iamarino','873.041.620-58','Praça Manuel Gomes Primo. Rua João de Barros, 222, Ferrazópolis','4127-9974',NULL,'Pesquisador Científico',1,NULL)
		,(3,'Drauzio Antônio Varella','514.607.220-51','Avenida José Odorizzi, 621, Bairro Assunção','(11) 98351-2563','drauzio@gmail.com','Médico',1,15000)
		,(6,'Vinícius Benevides da Silva','939.816.050-12','Avenida Jardim, 125, Vila Esperança',NULL,'vbenevides@termomecanica.com.br','Cientista de Dados',2,5000)

	INSERT INTO [dbo].[tb_Experiencia] ([Id_Curriculo],[Desc_Experiencia])
	VALUES
		 (1,'Comunicador científico de out de 2013 a set de 2021')
		,(1,'Postdoctoral Researcher de nov de 2014 - nov de 2015')
		,(2,'Médico atuante especialista')

	INSERT INTO [dbo].[tb_Formacao] ([Id_Curriculo],[Desc_Formacao])
	VALUES
		 (1,'Bacharel em Ciências na Universidade de São Paulo')
		,(2,'Medicina na Universidade de São Paulo')
		,(3,'Bacharel em Engenharia de Computação na Universidade Engenheiro Salvador Arena')
	
	INSERT INTO [dbo].[tb_Idioma] ([Id_Curriculo],[Desc_Idioma],[Grau_Proficiencia])
	VALUES
		 (1,'inglês',4)
		,(2,'Inglês',2)
		,(3,'Inglês',4)
		,(3,'Espanhol',3)
		,(3,'Libras',2)

END
GO


-- Seleção dos valores para validar
BEGIN

	SELECT * FROM [dbo].[tb_Area]
	SELECT * FROM [dbo].[tb_Curriculo]
	SELECT * FROM [dbo].[tb_Experiencia]
	SELECT * FROM [dbo].[tb_Formacao]
	SELECT * FROM [dbo].[tb_Idioma]

END
GO