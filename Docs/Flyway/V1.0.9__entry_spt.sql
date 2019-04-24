DROP PROCEDURE IF EXISTS dbo.sp_select_entry
GO
CREATE PROCEDURE sp_select_entry @Id int
    AS
    BEGIN
        SELECT * FROM entry WHERE id = @Id
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_selectlist_entry
GO
CREATE PROCEDURE sp_selectlist_entry
    AS
    BEGIN
        SELECT * FROM entry
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_insert_entry
GO
CREATE PROCEDURE sp_insert_entry
    (
        @category_id int = NULL ,
        @sub_category_id int = NULL ,
        @lexicon_function varchar(500) = NULL ,
        @recommendation varchar(500) = NULL ,
        @notes varchar(500) = NULL 
    )
    AS
    BEGIN
        INSERT INTO entry
        (category_id,sub_category_id,lexicon_function,recommendation,notes)
        VALUES
        (@category_id,@sub_category_id,@lexicon_function,@recommendation,@notes);
        SELECT SCOPE_IDENTITY();
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_update_entry
GO
CREATE PROCEDURE sp_update_entry
    (
        @id int = NULL ,
        @category_id int = NULL ,
        @sub_category_id int = NULL ,
        @lexicon_function varchar(500) = NULL ,
        @recommendation varchar(500) = NULL ,
        @notes varchar(500) = NULL 
    )
    AS
    BEGIN
        UPDATE entry SET
    category_id = @category_id,
    sub_category_id = @sub_category_id,
    lexicon_function = @lexicon_function,
    recommendation = @recommendation,
    notes = @notes
        WHERE id=@id;
    END
GO


DROP PROCEDURE IF EXISTS dbo.sp_delete_entry
GO
CREATE PROCEDURE sp_delete_entry(@id INT)
    AS
    BEGIN
        DELETE FROM entry
        WHERE id=@id;
    END
GO
