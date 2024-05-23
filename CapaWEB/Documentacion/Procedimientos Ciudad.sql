--procedimientos para la tabla de Ciudad

insert into Ciudad (Nombre, IdCiudad, IdDepartamento,idPais, Activo)
values ('Bogota',1, 1,1,1)
insert into Ciudad (Nombre, IdCiudad, IdDepartamento,idpais,  Activo)
values ('Duitama', 2,2,1,1)
insert into Ciudad (Nombre, IdCiudad, IdDepartamento,idPais, Activo)
values ('Paia', 2,2,2,1)

GO
create PROCEDURE SP_ConsultarCiudades 
	
AS
BEGIN
	select * from Ciudad
END
GO

create PROCEDURE SP_ConsultarCiudadPorDepartamento 
	@idDepartamento int,
	@idPais int
AS
BEGIN
	select * from Ciudad
	where IdDepartamento= @idDepartamento and idpais = @idPais
END
GO

create PROCEDURE SP_ConsultarCiudadPorID 
	@idCiudad int,
	@idDepartamento int,
	@idPais int
AS
BEGIN
	select * from Ciudad
	where IdCiudad= @idCiudad and IdDepartamento = @idDepartamento and idpais = @idPais
END
GO

create  PROCEDURE SP_InsertarCiudad 
	@Nombre varchar(500),
	@IdCiudad int,
	@idDepartamento int,
	@idPais int,
	@resultado int OUTPUT
AS
BEGIN
	insert into Ciudad (Nombre, IdCiudad, Activo, IdDepartamento, idPais)
	values (@Nombre,@IdCiudad, 1, @idDepartamento, @idPais )

	set @resultado= @@rowcount

END
GO
create PROCEDURE SP_ModificarCiudad 
	@Nombre varchar(500),
	@IdCiudad int,
	@idDepartamento int, 
	@idPais int,
	@resultado int OUTPUT
AS
BEGIN
	update Ciudad
	set Nombre=@Nombre
	where IdCiudad= @IdCiudad and IdDepartamento = @idDepartamento and  idPais= @idPais 

	set @resultado= @@rowcount

END
GO
create PROCEDURE SP_EliminarCiudad 
	@IdCiudad int,
	@idDepartamento int,
	@idPais int,
	@resultado int OUTPUT
AS
BEGIN
	update Ciudad
	set Activo = 0
	where IdCiudad= @IdCiudad and idDepartamento = @idDepartamento and idPais = @idPais

	set @resultado= @@rowcount

END
GO