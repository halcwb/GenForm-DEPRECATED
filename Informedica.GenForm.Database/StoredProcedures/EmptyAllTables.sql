-----------------------
Create Procedure dbo.sp_EmptyAllTables (@ResetIdentity Bit)

As

Begin

    Declare @SQL VarChar(500)
    Declare @TableName VarChar(255)
    Declare @ConstraintName VarChar(500)
    Declare curAllForeignKeys SCROLL CurSor For Select Table_Name,Constraint_Name From Information_Schema.Table_Constraints Where Constraint_Type='FOREIGN KEY'
    Open curAllForeignKeys

    Fetch Next From curAllForeignKeys INTO @TableName,@ConstraintName

    While @@FETCH_STATUS=0

    Begin
        Set @SQL = 'ALTER TABLE ' + @TableName + ' NOCHECK CONSTRAINT ' + @ConstraintName
        Execute(@SQL)
        Fetch Next From curAllForeignKeys INTO @TableName,@ConstraintName
    End

    Declare curAllTables Cursor For Select Table_Name From Information_Schema.Tables Where TABLE_TYPE='BASE TABLE'

    Open curAllTables
    
    Fetch Next From curAllTables INTO @TableName
    While @@FETCH_STATUS=0
    Begin
        Set @SQL = 'DELETE FROM [' + @TableName + '] '
            If @ResetIdentity = 1 AND OBJECTPROPERTY (OBJECT_ID(@TableName),'TableHasIdentity')=1
			Begin
                  --Set @SQL = @SQL + '; DBCC CHECKIDENT(''' + @TableName + ''',RESEED,0)'  
                  -- Only reseed tables when they are not virgin tables, otherwise reseed 0
                  -- will result in the first record having an identity of 0 instead of 1
                  Set @SQL = @SQL + '; IF NOT EXISTS(SELECT * FROM sys.identity_columns sic '
                  Set @SQL = @SQL + 'JOIN sys.tables st ON sic.object_id = st.object_id '
                  Set @SQL = @SQL + 'WHERE st.name = ''' + @TableName + ''' AND sic.last_value IS NULL) '
                  Set @SQL = @SQL + 'DBCC CHECKIDENT(''' + @TableName + ''',RESEED,0)'  
            End
        Execute(@SQL)
        Fetch Next From curAllTables INTO @TableName
    End

    Fetch First From curAllForeignKeys INTO @TableName,@ConstraintName
    While @@FETCH_STATUS=0

    Begin
        Set @SQL = 'ALTER TABLE ' + @TableName + ' CHECK CONSTRAINT ' + @ConstraintName
        Execute(@SQL)
        Fetch Next From curAllForeignKeys INTO @TableName,@ConstraintName
    End

    Close curAllTables  
    Deallocate curAllTables
    Close curAllForeignKeys
    Deallocate curAllForeignKeys  

End

-----------------------