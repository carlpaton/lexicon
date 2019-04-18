DROP PROCEDURE IF EXISTS dbo.sp_select_lexicon_entry_type
GO
CREATE PROCEDURE sp_select_lexicon_entry_type @Id int
    AS
    BEGIN
        SELECT * FROM lexicon_entry_type WHERE id = @Id
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_selectlist_lexicon_entry_type
GO
CREATE PROCEDURE sp_selectlist_lexicon_entry_type
    AS
    BEGIN
        SELECT * FROM lexicon_entry_type
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_insert_lexicon_entry_type
GO
CREATE PROCEDURE sp_insert_lexicon_entry_type
    (
        @description varchar(500) = NULL 
    )
    AS
    BEGIN
        INSERT INTO lexicon_entry_type
        (description)
        VALUES
        (@description);
        SELECT SCOPE_IDENTITY();
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_update_lexicon_entry_type
GO
CREATE PROCEDURE sp_update_lexicon_entry_type
    (
        @id int = NULL ,
        @description varchar(500) = NULL 
    )
    AS
    BEGIN
        UPDATE lexicon_entry_type SET
    description = @description
        WHERE id=@id;
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_delete_lexicon_entry_type
GO
CREATE PROCEDURE sp_delete_lexicon_entry_type(@id INT)
    AS
    BEGIN
        DELETE FROM lexicon_entry_type
        WHERE id=@id;
    END
GO
