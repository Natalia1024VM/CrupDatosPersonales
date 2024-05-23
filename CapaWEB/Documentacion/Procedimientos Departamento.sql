--procedimientos para la tabla de Departamento
insert into Departamento (Nombre, IdDepartamento, IdPais, Activo)
values ('Cundinamarca',1, 1,1)
insert into Departamento (Nombre, IdDepartamento, IdPais, Activo)
values ('Boyaca',2, 1,1)


insert into Departamento (Nombre, IdDepartamento, IdPais, Activo)
values ('Amazonas', 1,2,1)
insert into Departamento (Nombre, IdDepartamento, IdPais, Activo)
values ('Pará', 2,2,1)


GO
CREATE PROCEDURE SP_ConsultarDepartamento 
	
AS
BEGIN
	select * from Departamento
END
GO

CREATE PROCEDURE SP_ConsultarDepartamentoPorPais 
	@idPais int
AS
BEGIN
	select * from Departamento
	where IdPais= @idPais
END
GO

CREATE PROCEDURE SP_ConsultarDepartamentoPorID 
	@idDepartamento int,
	@idPais int
AS
BEGIN
	select * from Departamento
	where IdDepartamento= @idDepartamento and IdPais = @idPais
END
GO

CREATE PROCEDURE SP_InsertarDepartamento 
	@Nombre varchar(500),
	@IdDepartamento int,
	@idPais int,
	@resultado int OUTPUT
AS
BEGIN
	insert into Departamento (Nombre, IdDepartamento, Activo, IdPais)
	values (@Nombre,@IdDepartamento, 1, @idPais)

	set @resultado= @@rowcount

END
GO
CREATE PROCEDURE SP_ModificarDepartamento 
	@Nombre varchar(500),
	@IdDepartamento int,
	@idpais int, 
	@resultado int OUTPUT
AS
BEGIN
	update Departamento
	set Nombre=@Nombre
	where IdDepartamento= @IdDepartamento and IdPais = @idpais

	set @resultado= @@rowcount

END
GO
CREATE PROCEDURE SP_EliminarDepartamento 
	@IdDepartamento int,
	@idpais int,
	@resultado int OUTPUT
AS
BEGIN
	update Departamento
	set Activo = 0
	where IdDepartamento= @IdDepartamento and idpais = @idpais

	set @resultado= @@rowcount

END
GO