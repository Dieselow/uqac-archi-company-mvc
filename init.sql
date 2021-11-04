--CREATE DATABASE archi

USE archi
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'archi')      
     EXEC (N'CREATE SCHEMA archi')                                   
 GO                                                               

USE archi
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_accessadmin')      
     EXEC (N'CREATE SCHEMA db_accessadmin')                                   
 GO                                                               

USE archi
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_backupoperator')      
     EXEC (N'CREATE SCHEMA db_backupoperator')                                   
 GO                                                               

USE archi
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_datareader')      
     EXEC (N'CREATE SCHEMA db_datareader')                                   
 GO                                                               

USE archi
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_datawriter')      
     EXEC (N'CREATE SCHEMA db_datawriter')                                   
 GO                                                               

USE archi
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_ddladmin')      
     EXEC (N'CREATE SCHEMA db_ddladmin')                                   
 GO                                                               

USE archi
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_denydatareader')      
     EXEC (N'CREATE SCHEMA db_denydatareader')                                   
 GO                                                               

USE archi
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_denydatawriter')      
     EXEC (N'CREATE SCHEMA db_denydatawriter')                                   
 GO                                                               

USE archi
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_owner')      
     EXEC (N'CREATE SCHEMA db_owner')                                   
 GO                                                               

USE archi
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_securityadmin')      
     EXEC (N'CREATE SCHEMA db_securityadmin')                                   
 GO                                                               

USE archi
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'dbo')      
     EXEC (N'CREATE SCHEMA dbo')                                   
 GO                                                               

USE archi
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'guest')      
     EXEC (N'CREATE SCHEMA guest')                                   
 GO                                                               

USE archi
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'INFORMATION_SCHEMA')      
     EXEC (N'CREATE SCHEMA INFORMATION_SCHEMA')                                   
 GO                                                               

USE archi
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'sys')      
     EXEC (N'CREATE SCHEMA sys')                                   
 GO                                                               

USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'caregiver'  AND sc.name = N'archi'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'caregiver'  AND sc.name = N'archi'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [caregiver]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[caregiver]
(

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR according to character set mapping for latin1 character set
   */

   [licenceNumber] varchar(255)  NULL,
   [caregiverId] int IDENTITY(1,1) NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'archi.caregiver',
        N'SCHEMA', N'archi',
        N'TABLE', N'caregiver'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'consumable'  AND sc.name = N'archi'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'consumable'  AND sc.name = N'archi'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [consumable]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[consumable]
(
   [id] int IDENTITY(1,1) NOT NULL,
   [quantity] int  NULL,
   [threshold] int  NULL,
   [consumableTypeId] int  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'archi.consumable',
        N'SCHEMA', N'archi',
        N'TABLE', N'consumable'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'consumableType'  AND sc.name = N'archi'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'consumableType'  AND sc.name = N'archi'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [consumableType]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[consumableType]
(
   [id] int IDENTITY(1,1) NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR according to character set mapping for latin1 character set
   */

   [brand] varchar(255)  NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR according to character set mapping for latin1 character set
   */

   [name] varchar(255)  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'archi.consumableType',
        N'SCHEMA', N'archi',
        N'TABLE', N'consumableType'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'employee'  AND sc.name = N'archi'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'employee'  AND sc.name = N'archi'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [employee]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[employee]
(
   [employmentDate] datetime2(0)  NULL,
   [salary] float(24)  NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR according to character set mapping for latin1 character set
   */

   [workSchedule] varchar(255)  NULL,
   [employeeId] int IDENTITY(1,1) NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'archi.employee',
        N'SCHEMA', N'archi',
        N'TABLE', N'employee'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'equipment'  AND sc.name = N'archi'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'equipment'  AND sc.name = N'archi'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [equipment]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[equipment]
(
   [id] int  IDENTITY(1,1) NOT NULL,
   [installationDate] datetime2(0)  NULL,
   [equipmentTypeId] int  NULL,
   [roomId] int  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'archi.equipment',
        N'SCHEMA', N'archi',
        N'TABLE', N'equipment'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'EquipmentType'  AND sc.name = N'archi'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'EquipmentType'  AND sc.name = N'archi'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [EquipmentType]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[EquipmentType]
(
   [id] int IDENTITY(1,1) NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR according to character set mapping for latin1 character set
   */

   [name] varchar(255)  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'archi.EquipmentType',
        N'SCHEMA', N'archi',
        N'TABLE', N'EquipmentType'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'healthFile'  AND sc.name = N'archi'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'healthFile'  AND sc.name = N'archi'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [healthFile]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[healthFile]
(
   [id] int IDENTITY(1,1) NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR according to character set mapping for latin1 character set
   */

   [chronicConditions] varchar(255)  NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR according to character set mapping for latin1 character set
   */

   [emergencyContact] varchar(255)  NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR according to character set mapping for latin1 character set
   */

   [medications] varchar(255)  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'archi.healthFile',
        N'SCHEMA', N'archi',
        N'TABLE', N'healthFile'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'hibernate_sequence'  AND sc.name = N'archi'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'hibernate_sequence'  AND sc.name = N'archi'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [hibernate_sequence]
END 
GO

USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'patient'  AND sc.name = N'archi'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'patient'  AND sc.name = N'archi'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [patient]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[patient]
(
   [patientId] int IDENTITY(1,1) NOT NULL,
   [healthfileId] int  NULL,
   [primaryDoctorCaregiverId] int  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'archi.patient',
        N'SCHEMA', N'archi',
        N'TABLE', N'patient'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'room'  AND sc.name = N'archi'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'room'  AND sc.name = N'archi'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [room]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[room]
(
   [id] int IDENTITY(1,1) NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR according to character set mapping for latin1 character set
   */

   [name] varchar(255)  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'archi.room',
        N'SCHEMA', N'archi',
        N'TABLE', N'room'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'secretary'  AND sc.name = N'archi'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'secretary'  AND sc.name = N'archi'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [secretary]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[secretary]
(
   [secretaryId] int  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'archi.secretary',
        N'SCHEMA', N'archi',
        N'TABLE', N'secretary'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'ticket'  AND sc.name = N'archi'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'ticket'  AND sc.name = N'archi'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [ticket]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[ticket]
(
   [id] int IDENTITY(1,1) NOT NULL,
   [requestDate] datetime2(0)  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'archi.ticket',
        N'SCHEMA', N'archi',
        N'TABLE', N'ticket'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'ticketConsumables'  AND sc.name = N'archi'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'ticketConsumables'  AND sc.name = N'archi'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [ticketConsumables]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[ticketConsumables]
(
   [ticketId] int  NOT NULL,
   [consumableId] int  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'archi.ticketConsumables',
        N'SCHEMA', N'archi',
        N'TABLE', N'ticketConsumables'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user'  AND sc.name = N'archi'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'user'  AND sc.name = N'archi'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [user]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[user]
(
   [id] int IDENTITY(1,1) NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR according to character set mapping for latin1 character set
   */

   [address] varchar(255)  NULL,
   [dateOfBirth] datetime2(0)  NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR according to character set mapping for latin1 character set
   */

   [email] varchar(255)  NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR according to character set mapping for latin1 character set
   */

   [firstName] varchar(255)  NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR according to character set mapping for latin1 character set
   */

   [lastName] varchar(255)  NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR(MAX) according to character set mapping for latin1 character set
   */

   [password] varchar(max)  NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR according to character set mapping for latin1 character set
   */

   [phoneNumber] varchar(255)  NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0055: Data type was converted to VARCHAR according to character set mapping for latin1 character set
   */

   [username] varchar(255)  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'archi.`user`',
        N'SCHEMA', N'archi',
        N'TABLE', N'user'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO


USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_caregiver_caregiver_id'  AND sc.name = N'archi'  AND type in (N'PK'))
ALTER TABLE [caregiver] DROP CONSTRAINT [PK_caregiver_caregiver_id]
 GO



ALTER TABLE [caregiver]
 ADD CONSTRAINT [PK_caregiver_caregiver_id]
   PRIMARY KEY
   CLUSTERED ([caregiverId] ASC)

GO


USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_consumable_id'  AND sc.name = N'archi'  AND type in (N'PK'))
ALTER TABLE [consumable] DROP CONSTRAINT [PK_consumable_id]
 GO



ALTER TABLE [consumable]
 ADD CONSTRAINT [PK_consumable_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_consumableTypeId'  AND sc.name = N'archi'  AND type in (N'PK'))
ALTER TABLE [consumableType] DROP CONSTRAINT [PK_consumableTypeId]
 GO



ALTER TABLE [consumableType]
 ADD CONSTRAINT [PK_consumableTypeId]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_employee_employee_id'  AND sc.name = N'archi'  AND type in (N'PK'))
ALTER TABLE [employee] DROP CONSTRAINT [PK_employee_employee_id]
 GO



ALTER TABLE [employee]
 ADD CONSTRAINT [PK_employee_employee_id]
   PRIMARY KEY
   CLUSTERED ([employeeId] ASC)

GO


USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_equipment_id'  AND sc.name = N'archi'  AND type in (N'PK'))
ALTER TABLE [equipment] DROP CONSTRAINT [PK_equipment_id]
 GO



ALTER TABLE [equipment]
 ADD CONSTRAINT [PK_equipment_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_equipmentTypeId'  AND sc.name = N'archi'  AND type in (N'PK'))
ALTER TABLE [EquipmentType] DROP CONSTRAINT [PK_equipmentTypeId]
 GO



ALTER TABLE [EquipmentType]
 ADD CONSTRAINT [PK_equipmentTypeId]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_healthFileId'  AND sc.name = N'archi'  AND type in (N'PK'))
ALTER TABLE [healthFile] DROP CONSTRAINT [PK_healthFileId]
 GO



ALTER TABLE [healthFile]
 ADD CONSTRAINT [PK_healthFileId]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_patient_patient_id'  AND sc.name = N'archi'  AND type in (N'PK'))
ALTER TABLE [patient] DROP CONSTRAINT [PK_patient_patient_id]
 GO



ALTER TABLE [patient]
 ADD CONSTRAINT [PK_patient_patient_id]
   PRIMARY KEY
   CLUSTERED ([patientId] ASC)

GO


USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_roomId'  AND sc.name = N'archi'  AND type in (N'PK'))
ALTER TABLE [room] DROP CONSTRAINT [PK_roomId]
 GO



ALTER TABLE [room]
 ADD CONSTRAINT [PK_roomId]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_secretary_secretary_id'  AND sc.name = N'archi'  AND type in (N'PK'))
ALTER TABLE [secretary] DROP CONSTRAINT [PK_secretary_secretary_id]
 GO



ALTER TABLE [secretary]
 ADD CONSTRAINT [PK_secretary_secretary_id]
   PRIMARY KEY
   CLUSTERED ([secretaryId] ASC)

GO


USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_ticket_id'  AND sc.name = N'archi'  AND type in (N'PK'))
ALTER TABLE [ticket] DROP CONSTRAINT [PK_ticket_id]
 GO



ALTER TABLE [ticket]
 ADD CONSTRAINT [PK_ticket_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_ticket_consumables_ticket_id'  AND sc.name = N'archi'  AND type in (N'PK'))
ALTER TABLE [ticketConsumables] DROP CONSTRAINT [PK_ticket_consumables_ticket_id]
 GO



ALTER TABLE [ticketConsumables]
 ADD CONSTRAINT [PK_ticket_consumables_ticket_id]
   PRIMARY KEY
   CLUSTERED ([ticketId] ASC, [consumableId] ASC)

GO


USE archi
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_user_id'  AND sc.name = N'archi'  AND type in (N'PK'))
ALTER TABLE [user] DROP CONSTRAINT [PK_user_id]
 GO



ALTER TABLE [user]
 ADD CONSTRAINT [PK_user_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO






USE archi
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'ticketConsumables'  AND sc.name = N'archi'  AND si.name = N'FK4mh898wnxr7r5ult43nmk0n0y' AND so.type in (N'U'))
   DROP INDEX [FK4mh898wnxr7r5ult43nmk0n0y] ON [ticketConsumables] 
GO
CREATE NONCLUSTERED INDEX [FK4mh898wnxr7r5ult43nmk0n0y] ON [ticketConsumables]
(
   [consumableId] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE archi
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'patient'  AND sc.name = N'archi'  AND si.name = N'FK772m09qprd692almw4kcphy3f' AND so.type in (N'U'))
   DROP INDEX [FK772m09qprd692almw4kcphy3f] ON [patient] 
GO
CREATE NONCLUSTERED INDEX [FK772m09qprd692almw4kcphy3f] ON [patient]
(
   [primaryDoctorCaregiverId] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO


USE archi
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'equipment'  AND sc.name = N'archi'  AND si.name = N'FKog1e3ls88y65004cs34gtncgs' AND so.type in (N'U'))
   DROP INDEX [FKog1e3ls88y65004cs34gtncgs] ON [equipment] 
GO
CREATE NONCLUSTERED INDEX [FKog1e3ls88y65004cs34gtncgs] ON [equipment]
(
   [equipmentTypeId] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE archi
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'patient'  AND sc.name = N'archi'  AND si.name = N'FKphibxjbc32bf7bp35eexvcog0' AND so.type in (N'U'))
   DROP INDEX [FKphibxjbc32bf7bp35eexvcog0] ON [patient] 
GO
CREATE NONCLUSTERED INDEX [FKphibxjbc32bf7bp35eexvcog0] ON [patient]
(
   [healthFileId] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE archi
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'consumable'  AND sc.name = N'archi'  AND si.name = N'FKqoj1peb5pmpfs6v8168x9bfjg' AND so.type in (N'U'))
   DROP INDEX [FKqoj1peb5pmpfs6v8168x9bfjg] ON [consumable] 
GO
CREATE NONCLUSTERED INDEX [FKqoj1peb5pmpfs6v8168x9bfjg] ON [consumable]
(
   [consumableTypeId] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE archi
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'equipment'  AND sc.name = N'archi'  AND si.name = N'FKrlxmj3jqo3mnp532alg49ievg' AND so.type in (N'U'))
   DROP INDEX [FKrlxmj3jqo3mnp532alg49ievg] ON [equipment] 
GO
CREATE NONCLUSTERED INDEX [FKrlxmj3jqo3mnp532alg49ievg] ON [equipment]
(
   [roomId] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE archi
GO
ALTER TABLE  [caregiver]
 ADD DEFAULT NULL FOR [licenceNumber]
GO


USE archi
GO
ALTER TABLE  [consumable]
 ADD DEFAULT NULL FOR [quantity]
GO

ALTER TABLE  [consumable]
 ADD DEFAULT NULL FOR [threshold]
GO

ALTER TABLE  [consumable]
 ADD DEFAULT NULL FOR [consumableTypeId]
GO


USE archi
GO
ALTER TABLE  [consumableType]
 ADD DEFAULT NULL FOR [brand]
GO

ALTER TABLE  [consumableType]
 ADD DEFAULT NULL FOR [name]
GO


USE archi
GO
ALTER TABLE  [employee]
 ADD DEFAULT NULL FOR [employmentDate]
GO

ALTER TABLE  [employee]
 ADD DEFAULT NULL FOR [workSchedule]
GO


USE archi
GO
ALTER TABLE  [equipment]
 ADD DEFAULT NULL FOR [installationDate]
GO

ALTER TABLE  [equipment]
 ADD DEFAULT NULL FOR [equipmentTypeId]
GO

ALTER TABLE  [equipment]
 ADD DEFAULT NULL FOR [roomId]
GO


USE archi
GO
ALTER TABLE  [EquipmentType]
 ADD DEFAULT NULL FOR [name]
GO


USE archi
GO
ALTER TABLE  [healthFile]
 ADD DEFAULT NULL FOR [chronicConditions]
GO

ALTER TABLE  [healthFile]
 ADD DEFAULT NULL FOR [emergencyContact]
GO

ALTER TABLE  [healthFile]
 ADD DEFAULT NULL FOR [medications]
GO

USE archi
GO
ALTER TABLE  [patient]
 ADD DEFAULT NULL FOR [healthFileId]
GO

ALTER TABLE  [patient]
 ADD DEFAULT NULL FOR [primaryDoctorCaregiverId]
GO


USE archi
GO
ALTER TABLE  [room]
 ADD DEFAULT NULL FOR [name]
GO


USE archi
GO
ALTER TABLE  [ticket]
 ADD DEFAULT NULL FOR [requestDate]
GO


USE archi
GO
ALTER TABLE  [user]
 ADD DEFAULT NULL FOR [address]
GO

ALTER TABLE  [user]
 ADD DEFAULT NULL FOR [dateOfBirth]
GO

ALTER TABLE  [user]
 ADD DEFAULT NULL FOR [email]
GO

ALTER TABLE  [user]
 ADD DEFAULT NULL FOR [firstName]
GO

ALTER TABLE  [user]
 ADD DEFAULT NULL FOR [lastName]
GO

ALTER TABLE  [user]
 ADD DEFAULT NULL FOR [phoneNumber]
GO

ALTER TABLE  [user]
 ADD DEFAULT NULL FOR [username]
GO
