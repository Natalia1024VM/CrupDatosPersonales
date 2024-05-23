--procedimientos para la tabla de persona

insert into Persona (Nombre, Telefono, Direccion, CodigoCiudad, CodigoDepartamento, codigoPais)
values('Natalia', '317584520','CAlle', 1,1,1)
GO
CREATE PROCEDURE SP_ConsultarPersona 
	
AS
BEGIN
	select a.*, B.NombrePais, c.Nombre as 'NombreDep', D.Nombre as 'NombreCiu' from Persona A
	INNER JOIN Pais B ON A.CodigoPais = B.IdPais
	INNER JOIN Departamento c ON A.CodigoDepartamento = c.IdDepartamento and A.CodigoPais= c.IdPais
	INNER JOIN Ciudad D ON A.CodigoCiudad = D.IdCiudad and D.IdDepartamento = A.CodigoDepartamento and D.IdPais = A.codigoPais

END
GO

CREATE PROCEDURE SP_ConsultarPersonaNombre 
	@nombre varchar(200)
AS
BEGIN
	select a.*, B.NombrePais, c.Nombre as 'NombreDep', D.Nombre as 'NombreCiu' from Persona A
	INNER JOIN Pais B ON A.CodigoPais = B.IdPais
	INNER JOIN Departamento c ON A.CodigoDepartamento = c.IdDepartamento and A.CodigoPais= c.IdPais
	INNER JOIN Ciudad D ON A.CodigoCiudad = D.IdCiudad and D.IdDepartamento = A.CodigoDepartamento and D.IdPais = A.codigoPais
	where a.Nombre LIKE '%' + @nombre + '%'

END
GO

CREATE PROCEDURE SP_ConsultarPersonaPorID 
	@idPersona int
AS
BEGIN
	select a.*, B.NombrePais, c.Nombre as 'NombreDep', D.Nombre as 'NombreCiu' from Persona A
	INNER JOIN Pais B ON A.CodigoPais = B.IdPais
	INNER JOIN Departamento c ON A.CodigoDepartamento = c.IdDepartamento and A.CodigoPais= c.IdPais
	INNER JOIN Ciudad D ON A.CodigoCiudad = D.IdCiudad and D.IdDepartamento = A.CodigoDepartamento and D.IdPais = A.codigoPais
	where a.IdPersona= @idPersona
END
GO

CREATE PROCEDURE SP_InsertarPersona 
	@Nombre varchar(500),
	@Telefono varchar(10), 
	@Direccion varchar(500), 
	@CodigoCiudad int, 
	@CodigoDepartamento int, 
	@idpais int,
	@idPersona int OUTPUT
AS
BEGIN
	insert into Persona (Nombre,Telefono, Direccion, CodigoCiudad, CodigoDepartamento, codigoPais)
	values (@Nombre, @Telefono, @Direccion, @CodigoCiudad, @CodigoDepartamento, @idpais)

	select @idPersona = SCOPE_IDENTITY()
END
GO
CREATE PROCEDURE SP_ModificarPersona 
	@IdPersona int,
	@Nombre varchar(500),
	@Telefono varchar(10), 
	@Direccion varchar(500), 
	@CodigoCiudad int, 
	@CodigoDepartamento int, 
	@idpais int,
	@resultado int OUTPUT
AS
BEGIN
	update Persona
	set Nombre=@Nombre, Direccion = @Direccion, Telefono = @Telefono, CodigoCiudad = @CodigoCiudad, CodigoDepartamento = @CodigoDepartamento,codigoPais= @idpais 
	where IdPersona= @IdPersona

	set @resultado= @@rowcount

END
GO
CREATE PROCEDURE SP_EliminarPersona 
	@IdPersona int,
	@resultado int OUTPUT
AS
BEGIN
	delete from Persona
	where IdPersona = @IdPersona
	set @resultado= @@rowcount

END
GO