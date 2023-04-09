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