CREATE TABLE [dbo].[Educations] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [CvModelId]   INT            NOT NULL,
    [School]      NVARCHAR (255) NULL,
    [Degree]      INT		     NOT NULL,
    [StartYear]   DATE           NULL,
    [EndYear]     DATE           NULL,
    [CreatedBy]   INT            NULL,
    [UpdatedBy]   INT            NULL,
    [DeletedBy]   INT            NULL,
    [CreatedDate] DATETIME       DEFAULT (getdate()) NULL,
    [UpdatedDate] DATETIME       NULL,
    [DeletedDate] DATETIME       NULL,
    [IsDeleted]   BIT            DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Education_CvModel] FOREIGN KEY ([CvModelId]) REFERENCES [dbo].[CvModel] ([Id])
);

