USE [BDant2021]
GO
/****** Object:  StoredProcedure [dbo].[GetDocumentosList]    Script Date: 11/5/2023 03:10:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetDocumentosList]
            @secciondoc varchar(50),
			@cedula varchar(10)
        AS
        BEGIN
            SET NOCOUNT ON;

			declare @ltp_temporal table (id int, titulo varchar(200), obligatorio bit, cantidad int, nombredoc varchar(100), ruta varchar(200), cedula varchar(10)) 

			declare @TablaDocumentoSeccion table (Fila int, ID int, titulo varchar(200), cantidad int, descripcion varchar(511),obligatorio bit)

			INSERT INTO @TablaDocumentoSeccion (Fila, ID , titulo , cantidad , descripcion ,obligatorio)  SELECT row_number() OVER(ORDER BY id ASC) AS Fila, id,titulo,cantidad,descripcion,obligatorio FROM tbdocumentos t1 WHERE  EXISTS (SELECT * FROM (SELECT value as id FROM STRING_SPLIT(@secciondoc, ';')) t2 WHERE t1.id = t2.id)

			--DataTableTemporal
			DECLARE @CONTADORDATATABLE INT, @CONTADORWHILE INT, @IDDataTable INT
			DECLARE @ID int
			DECLARE @TITULO varchar(200)
			DECLARE @OBLIGATORIO bit
			DECLARE @CANTIDAD int
			DECLARE @NOMBREDOC varchar(100)
			DECLARE @RUTA varchar(200)
			DECLARE @CEDULATemp varchar(10)


			SET @CONTADORDATATABLE = (SELECT COUNT(*) FROM @TablaDocumentoSeccion)
			SET @CONTADORWHILE=0
			SET @IDDataTable = 0

			WHILE(@CONTADORWHILE < @CONTADORDATATABLE)BEGIN

				SET @IDDataTable = (SELECT TOP 1 Fila FROM @TablaDocumentoSeccion WHERE Fila > @IDDataTable)
				SET @ID = (SELECT TOP 1 ID FROM @TablaDocumentoSeccion WHERE Fila = @IDDataTable)
				SET @TITULO = (SELECT TOP 1 titulo FROM @TablaDocumentoSeccion WHERE Fila = @IDDataTable)
				SET @OBLIGATORIO = (SELECT TOP 1 obligatorio FROM @TablaDocumentoSeccion WHERE Fila = @IDDataTable)
				SET @CANTIDAD = (SELECT TOP 1 cantidad FROM @TablaDocumentoSeccion WHERE Fila = @IDDataTable)
				SET @NOMBREDOC = 'Vacio'
				SET @RUTA = ''
				SET @CEDULATemp = ''

				if (@CANTIDAD = 1) BEGIN
					if exists(Select * from ViewDocumentosSubidosUsuario where cedula = @cedula AND iddoc = @ID) BEGIN

						SET @NOMBREDOC = (Select nombredoc from ViewDocumentosSubidosUsuario where cedula = @cedula AND iddoc = @ID)
						SET @RUTA = (Select ruta from ViewDocumentosSubidosUsuario where cedula = @cedula AND iddoc = @ID)
						SET @CEDULATemp = (Select cedula from ViewDocumentosSubidosUsuario where cedula = @cedula AND iddoc = @ID)

					END
				END

				SET @CONTADORWHILE= @CONTADORWHILE+1

				INSERT INTO @ltp_temporal(id, titulo, obligatorio, cantidad, nombredoc, ruta, cedula) values (@ID, @TITULO, @OBLIGATORIO, @CANTIDAD, @NOMBREDOC, @RUTA, @CEDULATemp)

			END

			Select * from @ltp_temporal

			RETURN  
		END
