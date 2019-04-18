DROP PROCEDURE IF EXISTS dbo.sp_select_platform
GO
CREATE PROCEDURE sp_select_platform @Id int
    AS
    BEGIN
        SELECT * FROM platform WHERE id = @Id
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_selectlist_platform
GO
CREATE PROCEDURE sp_selectlist_platform
    AS
    BEGIN
        SELECT * FROM platform
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_insert_platform
GO
CREATE PROCEDURE sp_insert_platform
    (
        @description varchar(500) = NULL 
    )
    AS
    BEGIN
        INSERT INTO platform
        (description)
        VALUES
        (@description);
        SELECT SCOPE_IDENTITY();
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_update_platform
GO
CREATE PROCEDURE sp_update_platform
    (
        @id int = NULL ,
        @description varchar(500) = NULL 
    )
    AS
    BEGIN
        UPDATE platform SET
    description = @description
        WHERE id=@id;
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_delete_platform
GO
CREATE PROCEDURE sp_delete_platform(@id INT)
    AS
    BEGIN
        DELETE FROM platform
        WHERE id=@id;
    END
GO
