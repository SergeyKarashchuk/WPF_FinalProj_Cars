SET QUOTED_IDENTIFIER ON

DECLARE @sql varchar(max)
DECLARE @schema_version int, @id int


IF NOT EXISTS (SELECT 1 from sys.schemas s WHERE s.[name] = 'Admin')
BEGIN
    SET @sql = 'CREATE SCHEMA [Admin] AUTHORIZATION [dbo]'
    EXEC(@sql)
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Admin].[Options]') AND type in (N'U'))
	BEGIN
		CREATE TABLE [Admin].[Options]
		(
			ID INT IDENTITY(1, 1) NOT NULL,
			[SchemaVersion] INT NOT NULL,
			VersionDateTime DATETIME DEFAULT GETDATE(),
			CONSTRAINT [PK_Options] PRIMARY KEY CLUSTERED([ID])
		) ON [PRIMARY]
	END


BEGIN TRY

SET @schema_version = 1
IF NOT EXISTS (SELECT NULL FROM [Admin].[Options] WHERE [SchemaVersion] = @schema_version)
BEGIN
	BEGIN TRANSACTION
	
	INSERT INTO [Admin].[Options] ([SchemaVersion]) VALUES (@schema_version)
	COMMIT
END

SET @schema_version = 2
IF NOT EXISTS (SELECT NULL FROM [Admin].[Options] WHERE [SchemaVersion] = @schema_version)
BEGIN
	BEGIN TRANSACTION
	
	IF NOT EXISTS (SELECT 1 from sys.schemas s WHERE s.[name] = 'Cars')
	BEGIN
		SET @sql = 'CREATE SCHEMA [Cars] AUTHORIZATION [dbo]'
		EXEC(@sql)
	END

	CREATE TABLE Cars.BodyType
	(
		ID INT IDENTITY(1, 1) NOT NULL,
		[Name] NVARCHAR(100) NOT NULL,
		[Image] NVARCHAR(100) NULL,
		CONSTRAINT PK_BodyTypes PRIMARY KEY CLUSTERED (ID ASC)
	) ON [PRIMARY]
	CREATE UNIQUE INDEX IXU_BodyType_Name ON Cars.BodyType ([Name])

	EXEC sp_addextendedproperty N'MS_Description', N'Тип кузова',	N'SCHEMA', N'Cars', N'TABLE', N'BodyType'
	EXEC sp_addextendedproperty N'MS_Description', N'ID' ,			N'SCHEMA', N'Cars', N'TABLE', N'BodyType', N'COLUMN', N'ID'
	EXEC sp_addextendedproperty N'MS_Description', N'Название' ,	N'SCHEMA', N'Cars', N'TABLE', N'BodyType', N'COLUMN', N'Name'
	EXEC sp_addextendedproperty N'MS_Description', N'Картинка' ,	N'SCHEMA', N'Cars', N'TABLE', N'BodyType', N'COLUMN', N'Image'

	INSERT INTO [Admin].[Options] (SchemaVersion) VALUES (@schema_version)
	COMMIT
END

SET @schema_version = 3
IF NOT EXISTS (SELECT NULL FROM [Admin].[Options] WHERE [SchemaVersion] = @schema_version)
BEGIN
	BEGIN TRANSACTION
	
	CREATE TABLE Cars.Manufacturer
	(
		ID INT IDENTITY(1, 1) NOT NULL,
		[Name] NVARCHAR(100) NOT NULL,
		[Image] NVARCHAR(100) NULL,
		CONSTRAINT PK_Manufacturer PRIMARY KEY CLUSTERED (ID ASC)
	) ON [PRIMARY]
	CREATE UNIQUE INDEX IXU_Manufacturer_Name ON Cars.Manufacturer ([Name])

	EXEC sp_addextendedproperty N'MS_Description', N'Производитель',	N'SCHEMA', N'Cars', N'TABLE', N'Manufacturer'
	EXEC sp_addextendedproperty N'MS_Description', N'ID' ,				N'SCHEMA', N'Cars', N'TABLE', N'Manufacturer', N'COLUMN', N'ID'
	EXEC sp_addextendedproperty N'MS_Description', N'Название' ,		N'SCHEMA', N'Cars', N'TABLE', N'Manufacturer', N'COLUMN', N'Name'
	EXEC sp_addextendedproperty N'MS_Description', N'Картинка' ,		N'SCHEMA', N'Cars', N'TABLE', N'Manufacturer', N'COLUMN', N'Image'
	
	CREATE TABLE Cars.GearBoxType
	(
		ID INT IDENTITY(1, 1) NOT NULL,
		[Name] NVARCHAR(100) NOT NULL,
		[Image] NVARCHAR(100) NULL,
		CONSTRAINT PK_GearBoxType PRIMARY KEY CLUSTERED (ID ASC)
	) ON [PRIMARY]
	CREATE UNIQUE INDEX IXU_GearBoxType_Name ON Cars.GearBoxType ([Name])

	EXEC sp_addextendedproperty N'MS_Description', N'Тип коробки передач',	N'SCHEMA', N'Cars', N'TABLE', N'GearBoxType'
	EXEC sp_addextendedproperty N'MS_Description', N'ID' ,					N'SCHEMA', N'Cars', N'TABLE', N'GearBoxType', N'COLUMN', N'ID'
	EXEC sp_addextendedproperty N'MS_Description', N'Название' ,			N'SCHEMA', N'Cars', N'TABLE', N'GearBoxType', N'COLUMN', N'Name'
	EXEC sp_addextendedproperty N'MS_Description', N'Картинка' ,			N'SCHEMA', N'Cars', N'TABLE', N'GearBoxType', N'COLUMN', N'Image'

	CREATE TABLE Cars.WheelDriveType
	(
		ID INT IDENTITY(1, 1) NOT NULL,
		[Name] NVARCHAR(100) NOT NULL,
		[Image] NVARCHAR(100) NULL,
		CONSTRAINT PK_WheelDriveType PRIMARY KEY CLUSTERED (ID ASC)
	) ON [PRIMARY]
	CREATE UNIQUE INDEX IXU_WheelDriveType_Name ON Cars.GearBoxType ([Name])

	EXEC sp_addextendedproperty N'MS_Description', N'Тип привода',	N'SCHEMA', N'Cars', N'TABLE', N'WheelDriveType'
	EXEC sp_addextendedproperty N'MS_Description', N'ID' ,			N'SCHEMA', N'Cars', N'TABLE', N'WheelDriveType', N'COLUMN', N'ID'
	EXEC sp_addextendedproperty N'MS_Description', N'Название' ,	N'SCHEMA', N'Cars', N'TABLE', N'WheelDriveType', N'COLUMN', N'Name'
	EXEC sp_addextendedproperty N'MS_Description', N'Картинка' ,	N'SCHEMA', N'Cars', N'TABLE', N'WheelDriveType', N'COLUMN', N'Image'

	INSERT INTO [Admin].[Options] (SchemaVersion) VALUES (@schema_version)
	COMMIT
END

SET @schema_version = 4
IF NOT EXISTS (SELECT NULL FROM [Admin].[Options] WHERE [SchemaVersion] = @schema_version)
BEGIN
	BEGIN TRANSACTION
	
	CREATE TABLE Cars.Car
	(
		ID INT IDENTITY(1, 1) NOT NULL,
		BodyTypeID INT NULL,
		ManufacturerID INT NULL,
		GearBoxTypeID INT NULL,
		WheelDriveTypeID INT NULL,
		[Model] NVARCHAR(100) NULL,
		[Image] NVARCHAR(100) NULL,
		[Power] INT NULL, 
		[Price] NUMERIC(18,2) NULL,
		CONSTRAINT PK_Car PRIMARY KEY CLUSTERED (ID ASC),
		CONSTRAINT FK_Car_BodyType FOREIGN KEY (BodyTypeID) REFERENCES Cars.BodyType (ID),
		CONSTRAINT FK_Car_Manufacturer FOREIGN KEY (ManufacturerID) REFERENCES Cars.Manufacturer (ID),
		CONSTRAINT FK_Car_GearBoxType FOREIGN KEY (GearBoxTypeID) REFERENCES Cars.GearBoxType (ID),
		CONSTRAINT FK_Car_WheelDriveType FOREIGN KEY (WheelDriveTypeID) REFERENCES Cars.WheelDriveType (ID)

	) ON [PRIMARY]

	EXEC sp_addextendedproperty N'MS_Description', N'Автомобиль',				N'SCHEMA', N'Cars', N'TABLE', N'Car'
	EXEC sp_addextendedproperty N'MS_Description', N'ID' ,						N'SCHEMA', N'Cars', N'TABLE', N'Car', N'COLUMN', N'ID'
	EXEC sp_addextendedproperty N'MS_Description', N'Тип кузова' ,				N'SCHEMA', N'Cars', N'TABLE', N'Car', N'COLUMN', N'BodyTypeID'
	EXEC sp_addextendedproperty N'MS_Description', N'Производитель' ,			N'SCHEMA', N'Cars', N'TABLE', N'Car', N'COLUMN', N'ManufacturerID'
	EXEC sp_addextendedproperty N'MS_Description', N'Тип коробки передач' ,		N'SCHEMA', N'Cars', N'TABLE', N'Car', N'COLUMN', N'GearBoxTypeID'
	EXEC sp_addextendedproperty N'MS_Description', N'Тип привода' ,				N'SCHEMA', N'Cars', N'TABLE', N'Car', N'COLUMN', N'WheelDriveTypeID'
	EXEC sp_addextendedproperty N'MS_Description', N'Модель' ,					N'SCHEMA', N'Cars', N'TABLE', N'Car', N'COLUMN', N'Model'
	EXEC sp_addextendedproperty N'MS_Description', N'Картинка' ,				N'SCHEMA', N'Cars', N'TABLE', N'Car', N'COLUMN', N'Image'
	EXEC sp_addextendedproperty N'MS_Description', N'Мощность' ,				N'SCHEMA', N'Cars', N'TABLE', N'Car', N'COLUMN', N'Power'
	EXEC sp_addextendedproperty N'MS_Description', N'Цена' ,				N'SCHEMA', N'Cars', N'TABLE', N'Car', N'COLUMN', N'Price'
	
	INSERT INTO [Admin].[Options] (SchemaVersion) VALUES (@schema_version)
	COMMIT
END


/*
SET @schema_version = 5
IF NOT EXISTS (SELECT NULL FROM [Admin].[Options] WHERE [SchemaVersion] = @schema_version)
BEGIN
	BEGIN TRANSACTION

	INSERT INTO [Admin].[Options] (SchemaVersion) VALUES (@schema_version)
	COMMIT
END
*/
END TRY
BEGIN CATCH
	DECLARE @ErrMessage VARCHAR(MAX)
	SELECT @ErrMessage = CAST(ERROR_LINE() AS VARCHAR(10)) + ' - ' + ERROR_MESSAGE() +
		'. schema_version = ' + cast(@schema_version as varchar(10))
	ROLLBACK
	RAISERROR (@ErrMessage, 17, 1)
END CATCH
