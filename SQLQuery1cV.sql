CREATE TABLE Educations (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CvModelId INT NOT NULL,
    School NVARCHAR(255),
    Degree NVARCHAR(255),
    StartYear DATE,
    EndYear DATE,

    CreatedBy INT NULL,
    UpdatedBy INT NULL,
    DeletedBy INT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME NULL,
    DeletedDate DATETIME NULL,
    IsDeleted BIT DEFAULT 0,

    CONSTRAINT FK_Education_CvModel FOREIGN KEY (CvModelId) REFERENCES CvModel(Id)
);