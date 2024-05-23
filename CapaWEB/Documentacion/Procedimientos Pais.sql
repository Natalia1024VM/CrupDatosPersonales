--procedimientos para la tabla de pais
insert into Pais (NombrePais, IdPais, Activo)
values ('Colombia', 1,1)
insert into Pais (NombrePais, IdPais, Activo)
values ('Brazil', 2,0)

GO
CREATE PROCEDURE SP_ConsultarPais 
	
AS
BEGIN
	select * from Pais
END
GO

CREATE PROCEDURE SP_ConsultarPaisPorID 
	@idPais int
AS
BEGIN
	select * from Pais
	where IdPais= @idPais
END
GO

CREATE PROCEDURE SP_InsertarPais 
	@Nombre varchar(500),
	@IdPais int,
	@resultado int OUTPUT
AS
BEGIN
	insert into Pais (NombrePais, IdPais, Activo)
	values (@Nombre,@IdPais, 1)

	set @resultado= @@rowcount

END
GO
CREATE PROCEDURE SP_ModificarPais 
	@Nombre varchar(500),
	@IdPais int,
	@resultado int OUTPUT
AS
BEGIN
	update Pais
	set NombrePais=@Nombre
	where IdPais= @IdPais

	set @resultado= @@rowcount

END
GO
CREATE PROCEDURE SP_EliminarPais 
	@IdPais int,
	@resultado int OUTPUT
AS
BEGIN
	update Pais
	set Activo = 0
	where IdPais= @IdPais

	set @resultado= @@rowcount

END
GO