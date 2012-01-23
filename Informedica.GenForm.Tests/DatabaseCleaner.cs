using System;
using System.Text;
using Informedica.GenForm.Assembler;
using NHibernate;

namespace Informedica.GenForm.Tests
{
    // ToDo: Rewrite, this database cleaner only works for Microsoft Sql Server databases
    public static class DatabaseCleaner
    {
        private const string EmptyAllTables = 

@"USE [{0}]

Declare @ResetIdentity Bit
Set @ResetIdentity = {1}

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
";


        public static void CleanDataBase()
        {
            using (var session = GenFormApplication.SessionFactory.OpenSession())
            {
                session.Transaction.Begin();
                CleanDataBaseUsingSession(session);
                session.Transaction.Commit();
            }
        }

        private static void CleanDataBaseUsingSession(ISession session)
        {
            var query = session.CreateSQLQuery(GetEmptyAllTablesSql());
            query.ExecuteUpdate();
            query = session.CreateSQLQuery(RestoreSystemAdminSql());
            query.ExecuteUpdate();
        }

        private static string GetEmptyAllTablesSql()
        {
            return new StringBuilder().AppendFormat(EmptyAllTables, "GenFormTest", "1").ToString();
        }

        private static string RestoreSystemAdminSql()
        {
            var sql = new StringBuilder();
            sql.AppendFormat(
                @"INSERT INTO [GenFormTest].[dbo].[User]
           ([Id]
           ,[Version]
           ,[Name]
           ,[Email]
           ,[FirstName]
           ,[LastName]
           ,[Pager]
           ,[Password])
     VALUES
           ('{0}'
           ,1
           ,'Admin'
           ,'admin@gmail.com'
           ,'Admin'
           ,'Admin'
           ,'1'
           ,'Admin')", Guid.NewGuid());

            return sql.ToString();
        }

        public static void CleanDataBase(ISession session)
        {
            CleanDataBaseUsingSession(session);
        }
    }
}