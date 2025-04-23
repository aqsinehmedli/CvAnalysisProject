CREATE TABLE [dbo].[CvModel] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [UserId]       INT            NOT NULL,
    [FullName]     NVARCHAR (200) NOT NULL,
    [Email]        NVARCHAR (100) NOT NULL,
    [Phone]        NVARCHAR (50)  NOT NULL,
    [LinkedInUrl]  NVARCHAR (255) NULL,
    [GitHubUrl]    NVARCHAR (255) NULL,
    [TemplateType] INT            NULL,
    [CreatedBy]    INT            NULL,
    [UpdatedBy]    INT            NULL,
    [DeletedBy]    INT            NULL,
    [CreatedDate]  DATETIME       DEFAULT (getdate()) NOT NULL,
    [UpdatedDate]  DATETIME       NULL,
    [DeletedDate]  DATETIME       NULL,
    [IsDeleted]    BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CvModel_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

