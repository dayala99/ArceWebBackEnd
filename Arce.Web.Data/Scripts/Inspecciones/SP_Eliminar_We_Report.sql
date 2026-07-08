CREATE PROCEDURE SP_Eliminar_We_Report
    @We_Report_Id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Ins_We_Report
    SET Estado = 'I'
    WHERE We_Report_Id = @We_Report_Id;
END
GO
