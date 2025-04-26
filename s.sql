CREATE TABLE [dbo].[Experiences] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [CvModelId]   INT            NOT NULL,
    [Company]     NVARCHAR (255) NULL,
    [Position]    NVARCHAR (255) NULL,
    [Description]    NVARCHAR (255) NULL,
    [StartDate]   DATE           NULL,
    [EndDate]     DATE           NULL,
    [CreatedBy]   INT            NULL,
    [UpdatedBy]   INT            NULL,
    [DeletedBy]   INT            NULL,
    [CreatedDate] DATETIME       DEFAULT (getdate()) NULL,
    [UpdatedDate] DATETIME       NULL,
    [DeletedDate] DATETIME       NULL,
    [IsDeleted]   BIT            DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Experience_CvModel] FOREIGN KEY ([CvModelId]) REFERENCES [dbo].[CvModel] ([Id])
);
