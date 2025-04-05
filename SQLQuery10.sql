CREATE TABLE [dbo].[Experiences] (
    [Id]            INT             IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CvModelId]     INT             NOT NULL,
    [Company]       NVARCHAR(255)   NOT NULL,
    [Position]      NVARCHAR(255)   NOT NULL,
    [StartYear]     INT             NOT NULL,
    [EndYear]       INT,

    [CreatedBy]     NVARCHAR(255)   NOT NULL,
    [UpdatedBy]     NVARCHAR(255),
    [DeletedBy]     NVARCHAR(255),

    [CreatedDate]   DATETIME        NOT NULL DEFAULT GETDATE(),
    [UpdatedDate]   DATETIME,
    [DeletedDate]   DATETIME,

    [IsDeleted]     BIT             NOT NULL DEFAULT 0,

    CONSTRAINT FK_Experiences_CvModel FOREIGN KEY (CvModelId) REFERENCES [dbo].[CvModel](Id) ON DELETE CASCADE
);
