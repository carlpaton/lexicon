DROP PROCEDURE IF EXISTS dbo.sp_select_entry_platform
GO
CREATE PROCEDURE sp_select_entry_platform @Id int
    AS
    BEGIN
        SELECT * FROM entry_platform WHERE id = @Id
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_selectlist_entry_platform
GO
CREATE PROCEDURE sp_selectlist_entry_platform
    AS
    BEGIN
        SELECT * FROM entry_platform
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_insert_entry_platform
GO
CREATE PROCEDURE sp_insert_entry_platform
    (
        @entry_id int = NULL ,
        @platform_id int = NULL ,
        @description varchar(500) = NULL 
    )
    AS
    BEGIN
        INSERT INTO entry_platform
        (entry_id,platform_id,description)
        VALUES
        (@entry_id,@platform_id,@description);
        SELECT SCOPE_IDENTITY();
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_update_entry_platform
GO
CREATE PROCEDURE sp_update_entry_platform
    (
        @id int = NULL ,
        @entry_id int = NULL ,
        @platform_id int = NULL ,
        @description varchar(500) = NULL 
    )
    AS
    BEGIN
        UPDATE entry_platform SET
    entry_id = @entry_id,
    platform_id = @platform_id,
    description = @description
        WHERE id=@id;
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_delete_entry_platform
GO
CREATE PROCEDURE sp_delete_entry_platform(@id INT)
    AS
    BEGIN
        DELETE FROM entry_platform
        WHERE id=@id;
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_selectlist_entry_platform_by_entry_id
GO
CREATE PROCEDURE sp_selectlist_entry_platform_by_entry_id @Id int
    AS
    BEGIN
        SELECT * FROM entry_platform WHERE entry_id = @Id
    END
GO