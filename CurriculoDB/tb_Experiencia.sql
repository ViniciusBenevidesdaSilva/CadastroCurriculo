CREATE TABLE [dbo].[tb_Experiencia]
(
	 [Id_Experiencia]	INT				NOT NULL PRIMARY KEY IDENTITY(1,1)
	,[Id_Curriculo]		INT				NOT NULL FOREIGN KEY REFERENCES [dbo].[tb_Curriculo](Id_Curriculo)
	,[Desc_Experiencia]	VARCHAR(200)	NOT NULL
)