USE [AGfotografia]
GO

/****** Object:  StoredProcedure [dbo].[IngresoDatos]    Script Date: 23/06/2017 23:56:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[IngresoDatos] 
AS
    DECLARE @IngresoDatos TABLE(
		Dia			INT,
		Semana		INT,
		Mes			INT,
		Historico	INT
	);

	INSERT INTO @IngresoDatos (Dia)
		SELECT COUNT(*)
			FROM Ingreso I
			WHERE DATEPART(DD, I.FechaIngreso ) = DATEPART( DD, GETDATE() );

	UPDATE  @IngresoDatos 
		SET Semana =  ( SELECT	COUNT(*)
								FROM Ingreso I
								WHERE I.FechaIngreso <= GETDATE() AND I.FechaIngreso >= DATEADD(DAY,-7,GETDATE()) );


	UPDATE @IngresoDatos
		SET Mes = ( SELECT	COUNT(*)
							FROM Ingreso I
							WHERE DATEPART(MM, I.FechaIngreso ) = DATEPART( MM, GETDATE() ) );

	UPDATE @IngresoDatos
		SET Historico = ( SELECT COUNT(*)
								FROM Ingreso I )

	SELECT		Dia,	
				Semana,
				Mes,
				Historico
		FROM @IngresoDatos 
			
RETURN 0 

EXECUTE IngresoDatos
GO

