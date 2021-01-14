-- Seta a base
USE [BASE]
GO

-- Habilita a delimitação entre identificadores e literais das aspas em querys
SET QUOTED_IDENTIFIER ON
GO

-- Caso ocorra algum erro o rollback é automático
SET XACT_ABORT ON
GO

/******************* Criação de tabelas *******************/
BEGIN TRAN TABELAS

-- TblPessoas
CREATE TABLE [dbo].[tblPessoas](
	[nIdPessoa] [int] IDENTITY(1,1) NOT NULL,
	[sNome] [varchar](100) NOT NULL,
	[sNomeApelido] [varchar](20) NULL,
	[dNascimento] [date] NULL,
	[dInicio] [datetime] NOT NULL,
	[dAlteracao] [datetime] NOT NULL,
	[dFim] [datetime] NULL,
 CONSTRAINT [PK_tblPessoas] PRIMARY KEY CLUSTERED 
(
	[nIdPessoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[tblPessoas]  WITH CHECK ADD  CONSTRAINT [FK_tblPessoas_tblPessoas_Alt] FOREIGN KEY([nIdPessoaAlt])
REFERENCES [dbo].[tblPessoas] ([nIdPessoa])

ALTER TABLE [dbo].[tblPessoas] CHECK CONSTRAINT [FK_tblPessoas_tblPessoas_Alt]
GO

-- TblDocumentos
CREATE TABLE [dbo].[tblDocumentos](
	[nIdDocumento] [int] IDENTITY(1,1) NOT NULL,
	[nIdPessoa] [int] NOT NULL,
	[sDocumento] [varchar](100) NOT NULL,
	[dInicio] [datetime] NOT NULL,
	[dAlteracao] [datetime] NOT NULL,
	[dFim] [datetime] NULL,
 CONSTRAINT [PK_tblDocumentos] PRIMARY KEY CLUSTERED 
(
	[nIdDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[tblDocumentos]  WITH CHECK ADD  CONSTRAINT [FK_tblDocumentos_tblPessoas] FOREIGN KEY([nIdPessoa])
REFERENCES [dbo].[tblPessoas] ([nIdPessoa])

ALTER TABLE [dbo].[tblDocumentos] CHECK CONSTRAINT [FK_tblDocumentos_tblPessoas]
GO

-- TblEmails
CREATE TABLE [dbo].[tblEmails](
	[nIdEmail] [int] IDENTITY(1,1) NOT NULL,
	[nIdPessoa] [int] NOT NULL,
	[sEmail] [varchar](100) NOT NULL,
	[nValidado] [int] NOT NULL,
	[dInicio] [datetime] NOT NULL,
	[dAlteracao] [datetime] NOT NULL,
	[dFim] [datetime] NULL,
 CONSTRAINT [PK_tblEmails] PRIMARY KEY CLUSTERED 
(
	[nIdEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[tblEmails]  WITH CHECK ADD  CONSTRAINT [FK_tblEmails_tblPessoas] FOREIGN KEY([nIdPessoa])
REFERENCES [dbo].[tblPessoas] ([nIdPessoa])

ALTER TABLE [dbo].[tblEmails] CHECK CONSTRAINT [FK_tblEmails_tblPessoas]
GO

-- TblSenhas
CREATE TABLE [dbo].[tblSenhas](
	[nIdSenha] [int] IDENTITY(1,1) NOT NULL,
	[nIdPessoa] [int] NOT NULL,
	[sSenha] [varchar](100) NOT NULL,
	[dInicio] [datetime] NOT NULL,
	[dAlteracao] [datetime] NOT NULL,
	[dFim] [datetime] NULL,
 CONSTRAINT [PK_tblSenhas] PRIMARY KEY CLUSTERED 
(
	[nIdSenha] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[tblSenhas]  WITH CHECK ADD  CONSTRAINT [FK_tblSenhas_tblPessoas] FOREIGN KEY([nIdPessoa])
REFERENCES [dbo].[tblPessoas] ([nIdPessoa])

ALTER TABLE [dbo].[tblSenhas] CHECK CONSTRAINT [FK_tblSenhas_tblPessoas]
GO

COMMIT TRAN TABELAS