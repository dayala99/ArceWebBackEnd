CREATE TABLE Lg_Pedido_Cab
(
    Ped_Id INT,
    Ped_Usr_Apr VARCHAR(55),
    Ped_Lug_Ent VARCHAR(255),
    Ped_Ref VARCHAR(255),
    Ped_Tip_Com CHAR(2),
    Ped_Tip_Mon INT,
    Ped_Fec_Ent DATETIME,
    Ped_Sus VARCHAR(255),
    Ped_Arc_Adj_Nom VARCHAR(255),
    Ped_Arc_Adj_Rut VARCHAR(255),
    Ped_Prv_Cod INT,
    Ped_For_Pag_Cod INT,
    Ped_Tot NUMERIC(13, 3),
    Flg_Est CHAR(1),
    Usr_Reg VARCHAR(55),
    Fec_Reg DATETIME,
    Usr_Mod VARCHAR(55),
    Fec_Mod DATETIME
)
GO

--OBTENER NUEVO CORRELATIVO
--ESTE SE EJECUTA PRIMERO PARA PODER TENER EL CÓDIGO DE PEDIDO NUEVO
--EJECUTAR NI BIEN SE ABRE LA VENTANA NUEVO PEDIDO
CREATE PROCEDURE PA_Lg_Pedido_Cab_S0002
AS
BEGIN
    SELECT ISNULL(MAX(Ped_Id), 0) + 1 AS Ped_Id
    FROM Lg_Pedido_Cab 
END
GO

--ESTADOS P - PENDIENTE // A - APROBADO // C - CANCELADO 
CREATE PROCEDURE PA_Lg_Pedido_Cab_S0001
@Ped_Id INT
,   @Prv_Nom VARCHAR(255)
,   @Flg_Est CHAR(1)
,   @Ped_Tip_Com CHAR(2)
AS
BEGIN
    SELECT t1.Ped_Id, t1.Fec_Reg, t2.Prv_Nom,  1 AS Ped_Tip_Mon, t1.Ped_Tot,
        CASE 
            WHEN t1.Flg_Est = 'A' THEN 'APROBADO'
            WHEN t1.Flg_Est = 'P' THEN 'PENDIENTE'
            ELSE 'CANCELADO'
        END,
        t1.Usr_Reg, t1.Ped_Usr_Apr
    FROM Lg_Pedido_Cab t1
    JOIN Lg_Proveedor t2
    ON (t1.Ped_Prv_Cod = t2.Prv_Id)
    WHERE (@Ped_Id = 0 OR t1.Ped_Id = @Ped_Id)
    AND (@Prv_Nom = '' OR t2.Prv_Nom LIKE '%' + @Prv_Nom + '%')
    AND (@Flg_Est = '' OR t1.Flg_Est = @Flg_Est)
    AND (@Ped_Tip_Com = '' OR t1.Ped_Tip_Com = @Ped_Tip_Com)
END
GO



CREATE PROCEDURE PA_Lg_Pedido_Cab_I0001
@Ped_Id INT
,   @Ped_Usr_Apr VARCHAR(55)
,   @Ped_Lug_Ent VARCHAR(255)
,   @Ped_Ref VARCHAR(255)
,   @Ped_Tip_Com CHAR(2)
,   @Ped_Tip_Mon INT
,   @Ped_Fec_Ent DATETIME
,   @Ped_Sus VARCHAR(255)
,   @Ped_Arc_Adj_Nom VARCHAR(255)
,   @Ped_Arc_Adj_Rut VARCHAR(255)
,   @Ped_Prv_Cod INT
,   @Ped_For_Pag_Cod INT
,   @Usr_Reg VARCHAR(55)
,   @Codigo INT OUTPUT
,   @sMsj VARCHAR(55) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

            INSERT INTO Lg_Pedido_Cab (Ped_Id, Ped_Usr_Apr, Ped_Lug_Ent, Ped_Ref, Ped_Tip_Com, Ped_Tip_Mon, Ped_Fec_Ent,
            Ped_Sus, Ped_Arc_Adj_Nom, Ped_Arc_Adj_Rut, Ped_Prv_Cod, Ped_For_Pag_Cod, Flg_Est, Usr_Reg, Fec_Reg)
            VALUES (@Ped_Id, @Ped_Usr_Apr, @Ped_Lug_Ent, @Ped_Ref, @Ped_Tip_Com, @Ped_Tip_Mon, @Ped_Fec_Ent,
            @Ped_Sus, @Ped_Arc_Adj_Nom, @Ped_Arc_Adj_Rut, @Ped_Prv_Cod, @Ped_For_Pag_Cod, 'A', @Usr_Reg, GETDATE())

            SET @Codigo = 0
            SET @sMsj = 'REGISTRADO CORRECTAMENTE'

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION
        END

        SET @Codigo = ERROR_NUMBER()
        SET @sMsj = ERROR_MESSAGE()

    END CATCH
END
GO

CREATE PROCEDURE PA_Lg_Pedido_Cab_U0001
@Ped_Id INT
,   @Ped_Usr_Apr VARCHAR(55)
,   @Ped_Lug_Ent VARCHAR(255)
,   @Ped_Ref VARCHAR(255)
,   @Ped_Tip_Com CHAR(2)
,   @Ped_Tip_Mon INT
,   @Ped_Fec_Ent DATETIME
,   @Ped_Sus VARCHAR(255)
,   @Ped_Arc_Adj_Nom VARCHAR(255)
,   @Ped_Arc_Adj_Rut VARCHAR(255)
,   @Ped_Prv_Cod INT
,   @Ped_For_Pag_Cod INT
,   @Usr_Mod VARCHAR(55)
,   @Codigo INT OUTPUT
,   @sMsj VARCHAR(55) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
            UPDATE Lg_Pedido_Cab SET 
            Ped_Usr_Apr = @Ped_Usr_Apr
            ,   Ped_Lug_Ent = @Ped_Lug_Ent
            ,   Ped_Ref = @Ped_Ref
            ,   Ped_Tip_Com = @Ped_Tip_Com
            ,   Ped_Tip_Mon = @Ped_Tip_Mon
            ,   Ped_Fec_Ent = @Ped_Fec_Ent
            ,   Ped_Sus = @Ped_Sus
            ,   Ped_Arc_Adj_Nom = @Ped_Arc_Adj_Nom
            ,   Ped_Arc_Adj_Rut = @Ped_Arc_Adj_Rut
            ,   Ped_Prv_Cod = @Ped_Prv_Cod
            ,   Ped_For_Pag_Cod = @Ped_For_Pag_Cod
            ,   Usr_Mod = @Usr_Mod
            ,   Fec_Mod = GETDATE()
            WHERE Ped_Id = @Ped_Id

            SET @Codigo = 0
            SET @sMsj = 'ACTUALIZADO CORRECTAMENTE'
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION
        END

        SET @Codigo = ERROR_NUMBER()
        SET @sMsj = ERROR_MESSAGE()

    END CATCH
END
GO

CREATE PROCEDURE PA_Lg_Pedido_Cab_U0002
@Ped_Id INT
,   @Flg_Est CHAR(1)
,   @Codigo INT OUTPUT
,   @sMsj VARCHAR(55) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
            UPDATE Lg_Pedido_Cab SET Flg_Est = @Flg_Est
            WHERE Ped_Id = @Ped_Id
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION
        END

        SET @Codigo = ERROR_NUMBER()
        SET @sMsj = ERROR_MESSAGE()

    END CATCH   
END
GO

SELECT * FROM Lg_Pedido_Cab
GO
