ALTER TABLE [dbo].[Skills]
ADD 
    [StartYear]     INT,
    [EndYear]       INT,

    [CreatedBy]     NVARCHAR(255) NOT NULL DEFAULT 'system',
    [UpdatedBy]     NVARCHAR(255),
    [DeletedBy]     NVARCHAR(255),

    [CreatedDate]   DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedDate]   DATETIME,
    [DeletedDate]   DATETIME,

    [IsDeleted]     BIT NOT NULL DEFAULT 0;
