DROP PROCEDURE IF EXISTS dbo.sp_select_sub_category
GO
CREATE PROCEDURE sp_select_sub_category @Id int
    AS
    BEGIN
        SELECT * FROM sub_category WHERE id = @Id
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_selectlist_sub_category
GO
CREATE PROCEDURE sp_selectlist_sub_category
    AS
    BEGIN
        SELECT * FROM sub_category
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_insert_sub_category
GO
CREATE PROCEDURE sp_insert_sub_category
    (
        @description varchar(500) = NULL 
    )
    AS
    BEGIN
        INSERT INTO sub_category
        (description)
        VALUES
        (@description);
        SELECT SCOPE_IDENTITY();
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_update_sub_category
GO
CREATE PROCEDURE sp_update_sub_category
    (
        @id int = NULL ,
        @description varchar(500) = NULL 
    )
    AS
    BEGIN
        UPDATE sub_category SET
    description = @description
        WHERE id=@id;
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_delete_sub_category
GO
CREATE PROCEDURE sp_delete_sub_category(@id INT)
    AS
    BEGIN
        DELETE FROM sub_category
        WHERE id=@id;
    END
GO
