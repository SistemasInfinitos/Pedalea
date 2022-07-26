CREATE TABLE [dbo].[DetalleDocumentos](
	[DetalleDocumentoID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentoID] [int] NOT NULL,
	[ProductoID] [int] NOT NULL,
	[ValorUnitario] [decimal](18, 2) NOT NULL,
	[PorcentajeDescuento] [decimal](18, 2) NOT NULL,
)
ALTER TABLE [dbo].[DetalleDocumentos] add [Cantidad]decimal(18,4)not null default 1
ALTER TABLE [dbo].[DetalleDocumentos] add [FechaCreacion] [datetime] not null default getdate()
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
ALTER TABLE [dbo].[Documentos] add [FechaCreacion] [datetime] not null default getdate()
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
ALTER TABLE [dbo].[Inventarios] add [FechaCreacion] [datetime] not null default getdate()
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
ALTER TABLE [dbo].[Personas] add [FechaCreacion] [datetime] not null default getdate()


ALTER TABLE [dbo].[Personas] ADD  CONSTRAINT [unique_Persona] UNIQUE NONCLUSTERED (Identificacion)

Identificacion
CREATE TABLE [dbo].[Productos](
	[ProductoID] [int] IDENTITY(1,1) NOT NULL,
	[Producto] [varchar](50) NOT NULL,
	[Valor] [decimal](18, 2) NOT NULL,
	[Talla] [varchar](20) NOT NULL,
	[Color] [varchar](20) NOT NULL,
)
GO
ALTER TABLE [dbo].[Productos] add [FechaCreacion] [datetime] not null default getdate()
CREATE TABLE [dbo].[TipoDocumentos](
	[TipoDocumentoID] [int] IDENTITY(1,1) NOT NULL,
	[TipoDocumento] [varchar](50) NOT NULL,
)
--alter table Personas add Identificacion varchar(15)null
--alter table Documentos add Direccion varchar(150)null
go
create procedure SpGetPersonasById
@PersonaID int
as
begin 
 select * from Personas where PersonaID=@PersonaID
end
GO
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
ALTER procedure SpDeletePersona
@PersonaID int
as
BEGIN 
	if EXISTS(select COUNT(*)cantidad from Personas where PersonaID=@PersonaID)
	begin
		delete Personas where PersonaID=@PersonaID 
		select respuesta=@@ROWCOUNT; 
	end
	else 
		select respuesta=@@ROWCOUNT
	
END 


GO

create procedure SpGetDocumentos
as
begin 
 select * from Documentos 
end
GO

create procedure SpGetDocumentosById
@DocumentoID int
as
begin 
 select * from Documentos where DocumentoID=@DocumentoID
end
GO

ALTER procedure SpDeleteDocumento
@DocumentoID int
as
BEGIN 
	BEGIN TRANSACTION
		if exists(select * from Documentos where  DocumentoID=@DocumentoID)            
		BEGIN            
			DELETE I FROM Inventarios I JOIN DetalleDocumentos D ON D.DetalleDocumentoID=I.DetalleDocumentoID where  DocumentoID=@DocumentoID
			DELETE DetalleDocumentos where  DocumentoID=@DocumentoID
			DELETE Documentos where  DocumentoID=@DocumentoID
	IF(@@ERROR = 0)
		COMMIT 
	ELSE 
		ROLLBACK TRANSACTION
		End                    
		else            
			select respuesta=@@ROWCOUNT; 		  
		

	END
	
select * from  Documentos




create procedure SpGetDetalleDocumentos
as
begin 
 select * from DetalleDocumentos 
end
GO

create procedure SpGetDetalleDocumentosById
@DetalleDocumentoID int
as
begin 
 select * from DetalleDocumentos where DetalleDocumentoID=@DetalleDocumentoID
end
GO
SpGetProductosByName @name ='RI',@ProductoID=1

alter procedure SpGetProductosByName
@name varchar(50),
@ProductoID int=0
as
begin 
 if(@ProductoID)=0
 begin
	select * from Productos where Producto like ('%'+@name+'%')
 end 
	else
	select * from Productos where ProductoID =@ProductoID
end
GO
select * from Productos where Producto like ('%a%')OR ProductoID = 1
SpCrearDocumento
	@PersonaIDCliente =null,
	@PersonaIDVendedor =null,
	@PrimerNombre =null,
	@SegundoNombre =null,
	@PrimerApellido =null,
	@SegundoApellido =null,
	@EsCliente =null,
	@EsProveedor =null,
	@Cantidad =null,
	@Identificacion =null,
	@Direccion =null,
	@DocumentoID =null,
	@TipoDocumentoID =null,
	@ValorTotal =null,
	@ProductoID =null,
	@ValorUnitario  =null,
	@PorcentajeDescuento =null,
	@IdOutPut =null,


ALTER PROCEDURE [dbo].[SpCrearDocumento]
	@PersonaIDCliente int=null,
	@PersonaIDVendedor int,
	@PrimerNombre varchar(50)=null,
	@SegundoNombre varchar(50)=null,
	@PrimerApellido varchar(50)=null,
	@SegundoApellido varchar(50)=null,
	@EsCliente bit=1,
	@EsProveedor bit=0,
	@Cantidad int,
	@Identificacion varchar(15),
	@Direccion nvarchar(150),
	@DocumentoID int=0,
	@TipoDocumentoID INT=2,
	@ValorTotal DECIMAL(18,2),
	@ProductoID INT,
	@ValorUnitario  DECIMAL(18,2),
	@PorcentajeDescuento DECIMAL(18,2),
	@IdOutPut INT OUTPUT

AS

BEGIN
	
	BEGIN TRANSACTION
	DECLARE @actualizar int=0;
	DECLARE @DetalleDocumentoID int;
	if exists(select * from Personas where  Identificacion=@Identificacion)            
		BEGIN            
			UPDATE Personas SET 
			PrimerNombre=@PrimerNombre,
			SegundoNombre=@SegundoNombre,
			PrimerApellido=@PrimerApellido,
			SegundoApellido=@SegundoApellido,
			EsCliente=@EsCliente,
			EsProveedor=@EsProveedor
		 where PersonaID=@PersonaIDCliente OR Identificacion=@Identificacion 
		 SET @PersonaIDCliente =(SELECT PersonaID FROM Personas where Identificacion=@Identificacion )
		End                    
		else            
		begin  
			INSERT INTO Personas(PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,EsCliente,EsProveedor,Identificacion)
			VALUES(@PrimerNombre,@SegundoNombre,@PrimerApellido,@SegundoApellido,@EsCliente,@EsProveedor,@Identificacion)
			SET @PersonaIDCliente= SCOPE_IDENTITY();			  
		end
		
		
		if exists(SELECT * FROM Documentos where DocumentoID= @DocumentoID)            
		BEGIN            
			UPDATE Documentos SET 
			PersonaIDCliente=@PersonaIDCliente,
			PersonaIDVendedor=@PersonaIDVendedor,
			TipoDocumentoID=@TipoDocumentoID,
			ValorTotal=@ValorTotal,
			Direccion=@Direccion
			where DocumentoID=@DocumentoID 
		-- SET @DocumentoID =(SELECT DocumentoID FROM Documentos where DocumentoID=@DocumentoID )
		 SET @actualizar=1;
		End                    
		else            
		begin  
			INSERT INTO Documentos(PersonaIDCliente,PersonaIDVendedor,TipoDocumentoID,ValorTotal,Direccion)
			VALUES(@PersonaIDCliente,@PersonaIDVendedor,@TipoDocumentoID,@ValorTotal,@Direccion)
			SET @DocumentoID= SCOPE_IDENTITY();	
			SET @actualizar=0;		  
		end
				 
		SET @IdOutPut = @DocumentoID;

		INSERT INTO DetalleDocumentos(
			DocumentoID,
			ProductoID,
			ValorUnitario,
			PorcentajeDescuento,
			Cantidad
		)VALUES(
			@DocumentoID,@ProductoID,@ValorUnitario, @PorcentajeDescuento,@Cantidad
		)
		SET @DetalleDocumentoID = SCOPE_IDENTITY();	

				
		IF(@TipoDocumentoID)=2
		BEGIN	
			INSERT INTO Inventarios(ProductoID,DetalleDocumentoID,Entrante,Saliente,Separado)	
			VALUES(@ProductoID,@DetalleDocumentoID,0,@Cantidad,0)
		END ELSE
		IF(@TipoDocumentoID)=1
		BEGIN	
			INSERT INTO Inventarios(ProductoID,DetalleDocumentoID,Entrante,Saliente,Separado)	
			VALUES(@ProductoID,@DetalleDocumentoID,@Cantidad,0,0)
		END ELSE
			INSERT INTO Inventarios(ProductoID,DetalleDocumentoID,Entrante,Saliente,Separado)	
			VALUES(@ProductoID,@DetalleDocumentoID,0,0,@Cantidad)

		IF(@actualizar)=1
		begin 
			SET @ValorTotal= (SELECT valor=SUM((ValorUnitario-((ValorUnitario*PorcentajeDescuento)/100))*Cantidad) FROM DetalleDocumentos where DocumentoID=@DocumentoID )
			UPDATE Documentos SET ValorTotal= @ValorTotal where DocumentoID=@DocumentoID  
			end 
		--ELSE 
			--SELECT 0 
	SELECT DocumentoID= @IdOutPut
	IF(@@ERROR = 0)
		COMMIT 
	ELSE 
		ROLLBACK TRANSACTION
	END

GO


SELECT * FROM Personas
SELECT * FROM Documentos WHERE DocumentoID=17
SELECT * FROM DetalleDocumentos WHERE DocumentoID=17
select total= (SELECT valor=SUM((ValorUnitario-((ValorUnitario*PorcentajeDescuento)/100))*Cantidad) FROM DetalleDocumentos where DocumentoID=17)--@DocumentoID )
			

SpCrearDocumento @PersonaIDVendedor=15 ,@PrimerNombre='Misael Bello',@PersonaIDCliente=0 
,@Identificacion='1234567',@Direccion='CALLE 12-34',@ValorTotal=50000,@ProductoID=2,
@ValorUnitario =50000, @PorcentajeDescuento=2,@DocumentoID=30,@Cantidad=2,@IdOutPut=0


SELECT * FROM Personas
SELECT * FROM Documentos WHERE DocumentoID=42
SELECT * FROM DetalleDocumentos WHERE DocumentoID=42
SELECT * FROM Inventarios WHERE DetalleDocumentoID in(71,72,73)



CREATE TYPE dbo.LisDetalleDocumentos AS TABLE
( 
	ProductoID int null, 
	Producto nvarchar(50) null, 
	Valor decimal (18,2) null,
	Talla varchar (50) null,
	Color varchar (50) null,
	FechaCreacion datetime null default getdate(),
	ValorUnitario decimal (18,2) null,
	Cantidad decimal (18,2) null,
	PorcentajeDescuento decimal (18,2) null,
	Neto decimal (18,2) null
)

SpCrearDocumento
    @PersonaIDCliente = 0,
	@PersonaIDVendedor = 23,
	@PrimerNombre = 'Mauricio 7',
	@SegundoNombre = 'a',
	@PrimerApellido = 'b',
	@SegundoApellido = 'c',
	@EsCliente = 1,
	@EsProveedor = 1,
	@Cantidad = 1,
	@Identificacion = '1234567',
	@Direccion = 'calle80',
	@DocumentoID = 0,
	@TipoDocumentoID = 2,
	@ValorTotal = 45500,
	@ProductoID = 2,
	@ValorUnitario = 20500,
	@PorcentajeDescuento = 0,
	@IdOutPut = 0