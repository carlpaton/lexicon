DROP PROCEDURE IF EXISTS dbo.sp_select_category
GO
CREATE PROCEDURE sp_select_category @Id int
    AS
    BEGIN
        SELECT * FROM category WHERE id = @Id
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_selectlist_category
GO
CREATE PROCEDURE sp_selectlist_category
    AS
    BEGIN
        SELECT * FROM category
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_insert_category
GO
CREATE PROCEDURE sp_insert_category
    (
        @description varchar(500) = NULL 
    )
    AS
    BEGIN
        INSERT INTO category
        (description)
        VALUES
        (@description);
        SELECT SCOPE_IDENTITY();
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_update_category
GO
CREATE PROCEDURE sp_update_category
    (
        @id int = NULL ,
        @description varchar(500) = NULL 
    )
    AS
    BEGIN
        UPDATE category SET
    description = @description
        WHERE id=@id;
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_delete_category
GO
CREATE PROCEDURE sp_delete_category(@id INT)
    AS
    BEGIN
        DELETE FROM category
        WHERE id=@id;
    END
GO
