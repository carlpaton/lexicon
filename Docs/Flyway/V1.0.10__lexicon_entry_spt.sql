DROP PROCEDURE IF EXISTS dbo.sp_select_lexicon_entry
GO
CREATE PROCEDURE sp_select_lexicon_entry @Id int
    AS
    BEGIN
        SELECT * FROM lexicon_entry WHERE id = @Id
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_selectlist_lexicon_entry
GO
CREATE PROCEDURE sp_selectlist_lexicon_entry
    AS
    BEGIN
        SELECT * FROM lexicon_entry
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_insert_lexicon_entry
GO
CREATE PROCEDURE sp_insert_lexicon_entry
    (
        @category_id int = NULL ,
        @platform_id int = NULL ,
        @sub_category_id int = NULL ,
        @lexicon_entry_type_id int = NULL ,
        @description varchar(500) = NULL 
    )
    AS
    BEGIN
        INSERT INTO lexicon_entry
        (category_id,platform_id,sub_category_id,lexicon_entry_type_id,description)
        VALUES
        (@category_id,@platform_id,@sub_category_id,@lexicon_entry_type_id,@description);
        SELECT SCOPE_IDENTITY();
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_update_lexicon_entry
GO
CREATE PROCEDURE sp_update_lexicon_entry
    (
        @id int = NULL ,
        @category_id int = NULL ,
        @platform_id int = NULL ,
        @sub_category_id int = NULL ,
        @lexicon_entry_type_id int = NULL ,
        @description varchar(500) = NULL 
    )
    AS
    BEGIN
        UPDATE lexicon_entry SET
    category_id = @category_id,
    platform_id = @platform_id,
    sub_category_id = @sub_category_id,
    lexicon_entry_type_id = @lexicon_entry_type_id,
    description = @description
        WHERE id=@id;
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_delete_lexicon_entry
GO
CREATE PROCEDURE sp_delete_lexicon_entry(@id INT)
    AS
    BEGIN
        DELETE FROM lexicon_entry
        WHERE id=@id;
    END
GO
