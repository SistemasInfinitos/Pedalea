CREATE TABLE [dbo].[DetalleDocumentos](
	[DetalleDocumentoID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentoID] [int] NOT NULL,
	[ProductoID] [int] NOT NULL,
	[ValorUnitario] [decimal](18, 2) NOT NULL,
	[PorcentajeDescuento] [decimal](18, 2) NOT NULL,
)
ALTER TABLE [dbo].[DetalleDocumentos]  WITH CHECK ADD  CONSTRAINT [FK_DetalleDocumentosDocumen] FOREIGN KEY([DocumentoID])
REFERENCES [dbo].[Documentos] ([DocumentoID])
ALTER TABLE [dbo].[DetalleDocumentos]  WITH CHECK ADD  CONSTRAINT [FK_DetalleDocumentosProduc] FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Productos] ([ProductoID])
GO

CREATE TABLE [dbo].[Documentos](
	[DocumentoID] [int] IDENTITY(1,1) NOT NULL,
	[PersonaIDCliente] [int] NOT NULL,
	[PersonaIDVendedor] [int] NOT NULL,
	--[PersonaIDNit] [int] NOT NULL,
	[TipoDocumentoID] [int] NOT NULL,
	[ValorTotal] [decimal](18, 2) NOT NULL,
	--[Talla] [varchar](20) NOT NULL,
	--[Color] [varchar](20) NOT NULL,
	[Direccion] [varchar](150) NULL,
)
--alter table Documentos drop column Talla
--alter table Documentos drop column PersonaIDNit
ALTER TABLE [dbo].[Documentos]  WITH CHECK ADD  CONSTRAINT [FK_DocumentosTipoDocumentos] FOREIGN KEY([TipoDocumentoID])
REFERENCES [dbo].[TipoDocumentos] ([TipoDocumentoID])
--ALTER TABLE [dbo].[Documentos]  WITH CHECK ADD  CONSTRAINT [FK_InventariosDetalleEmresa] FOREIGN KEY([PersonaIDNit])
--REFERENCES [dbo].[Personas] ([PersonaID])
ALTER TABLE [dbo].[Documentos]  WITH CHECK ADD  CONSTRAINT [FK_InventariosDetalleVendedor] FOREIGN KEY([PersonaIDVendedor])
REFERENCES [dbo].[Personas] ([PersonaID])
GO

CREATE TABLE [dbo].[Inventarios](
	[InventarioID] [int] IDENTITY(1,1) NOT NULL,
	[ProductoID] [int] NOT NULL,
	[DetalleDocumentoID] [int] NOT NULL,
	[Entrante] [decimal](18, 6) NOT NULL,
	[Saliente] [decimal](18, 6) NOT NULL,
	[Separado] [decimal](18, 6) NOT NULL,
)
ALTER TABLE [dbo].[Inventarios]  WITH CHECK ADD  CONSTRAINT [FK_Inventariosdetalle] FOREIGN KEY([ProductoID])
REFERENCES [dbo].[DetalleDocumentos] ([DetalleDocumentoID])
ALTER TABLE [dbo].[Inventarios]  WITH CHECK ADD  CONSTRAINT [FK_InventariosDetalleDocumentos] FOREIGN KEY([DetalleDocumentoID])
REFERENCES [dbo].[DetalleDocumentos] ([DetalleDocumentoID])
ALTER TABLE [dbo].[Inventarios]  WITH CHECK ADD  CONSTRAINT [FK_InventariosProductos] FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Productos] ([ProductoID])
GO

CREATE TABLE [dbo].[Personas](
	[PersonaID] [int] IDENTITY(1,1) NOT NULL,
	[PrimerNombre] [varchar](50)  NULL,
	[SegundoNombre] [varchar](50) NULL,
	[PrimerApellido] [varchar](50)  NULL,
	[SegundoApellido] [varchar](50) NULL,
	[EsCliente] [bit] NOT NULL,
	[EsProveedor] [bit] NOT NULL,	
	[Identificacion] [varchar](15) NULL,
)
GO

CREATE TABLE [dbo].[Productos](
	[ProductoID] [int] IDENTITY(1,1) NOT NULL,
	[Producto] [varchar](50) NOT NULL,
	[Valor] [decimal](18, 2) NOT NULL,
	[Talla] [varchar](20) NOT NULL,
	[Color] [varchar](20) NOT NULL,
)

GO
CREATE TABLE [dbo].[TipoDocumentos](
	[TipoDocumentoID] [int] IDENTITY(1,1) NOT NULL,
	[TipoDocumento] [varchar](50) NOT NULL,
)
--alter table Personas add Identificacion varchar(15)null
--alter table Documentos add Direccion varchar(150)null
go
create procedure SpGetPersonas
as
begin 
 select * from Personas 
end

GO

alter procedure SpInserPersona
@PersonaID int,
@PrimerNombre varchar(50),
@SegundoNombre varchar(50),
@PrimerApellido varchar(50),
@SegundoApellido varchar(50),
@EsCliente bit,
@EsProveedor bit,
@Identificacion varchar(15)
as
begin 
--declare @id int =1--prueba 
if EXISTS(select COUNT(*) from Personas where PersonaID!=@PersonaID)
	begin
	insert into Personas(PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,EsCliente,EsProveedor,Identificacion)
	values(@PrimerNombre,@SegundoNombre,@PrimerApellido,@SegundoApellido,@EsCliente,@EsProveedor,@Identificacion)
	select respuesta=@@ROWCOUNT; 
	end
else 
	select respuesta=@@ROWCOUNT
end
GO

alter procedure SpUpdatePersona
@PersonaID int,
@PrimerNombre varchar(50),
@SegundoNombre varchar(50),
@PrimerApellido varchar(50),
@SegundoApellido varchar(50),
@EsCliente bit,
@EsProveedor bit,
@Identificacion varchar(15)
as
begin 
--declare @id int =1--prueba 
if EXISTS(select COUNT(*) from Personas where PersonaID=@PersonaID)
	begin
	UPDATE Personas SET 
	PrimerNombre=@PrimerNombre,
	SegundoNombre=@SegundoNombre,
	PrimerApellido=@PrimerApellido,
	SegundoApellido=@SegundoApellido,
	EsCliente=@EsCliente,
	EsProveedor=@EsProveedor,
	Identificacion=@Identificacion where PersonaID=@PersonaID

	select respuesta=@@ROWCOUNT; 
	end
else 
	select respuesta=@@ROWCOUNT
end

GO
create procedure SpDeletePersona
@PersonaID int
as
begin 
--declare @id int =1--prueba 
if EXISTS(select COUNT(*)cantidad from Personas where PersonaID=@PersonaID)
	begin
	delete Personas where PersonaID=@PersonaID 
	select respuesta=@@ROWCOUNT; 
	end
else 
	select respuesta=@@ROWCOUNT
end

GO


select * from Personas