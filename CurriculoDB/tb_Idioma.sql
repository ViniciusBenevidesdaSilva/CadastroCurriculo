CREATE TABLE [dbo].[tb_Idioma]
(
	 [Id_Idioma]			INT				NOT NULL PRIMARY KEY IDENTITY(1,1)
	,[Id_Curriculo]			INT				NOT NULL FOREIGN KEY REFERENCES [dbo].[tb_Curriculo](Id_Curriculo)
	,[Desc_Idioma]			VARCHAR(200)	NOT NULL
	,[Grau_Proficiencia]	INT	NOT NULL
)