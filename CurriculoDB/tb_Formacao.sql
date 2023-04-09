CREATE TABLE [dbo].[tb_Formacao]
(
	 [Id_Formacao]		INT			NOT NULL PRIMARY KEY IDENTITY(1,1)
	,[Id_Curriculo]		INT			NOT NULL FOREIGN KEY REFERENCES [dbo].[tb_Curriculo](Id_Curriculo)
	,[Desc_Formacao]	VARCHAR(200)	NOT NULL
)