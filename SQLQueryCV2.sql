CREATE TABLE Skills (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CvModelId INT NOT NULL,
    SkillName NVARCHAR(100),
    ProficiencyLevel INT NOT NULL,  -- Burada ProficiencyLevel sütunu ekleniyor

    CreatedBy INT NULL,
    UpdatedBy INT NULL,
    DeletedBy INT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME NULL,
    DeletedDate DATETIME NULL,
    IsDeleted BIT DEFAULT 0,

    CONSTRAINT FK_Skill_CvModel FOREIGN KEY (CvModelId) REFERENCES CvModel(Id)
);
