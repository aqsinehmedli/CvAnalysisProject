CREATE TABLE CvModel (
    Id INT PRIMARY KEY IDENTITY, -- Otomatik artan birincil anahtar
    UserId INT NOT NULL, -- UserId dış anahtar
    FullName NVARCHAR(200) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(50) NOT NULL,
    LinkedInUrl NVARCHAR(255),
    GitHubUrl NVARCHAR(255),
    TemplateName NVARCHAR(100),
    CreatedBy INT, -- CreatedBy: kim yaratmış
    UpdatedBy INT, -- UpdatedBy: kim güncellemiş
    DeletedBy INT, -- DeletedBy: kim silmiş
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(), -- Oluşturulma tarihi
    UpdatedDate DATETIME, -- Güncellenme tarihi
    DeletedDate DATETIME, -- Silinme tarihi
    IsDeleted BIT NOT NULL DEFAULT 0, -- Silinmiş mi? (0 = hayır, 1 = evet)

    -- Foreign Key (Dış Anahtar)
    CONSTRAINT FK_CvModel_User FOREIGN KEY (UserId) REFERENCES Users(Id)
);
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

CREATE TABLE Experiences (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CvModelId INT NOT NULL,
    Company NVARCHAR(255),
    Position NVARCHAR(255),
    StartDate DATE,
    EndDate DATE,

    CreatedBy INT NULL,
    UpdatedBy INT NULL,
    DeletedBy INT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME NULL,
    DeletedDate DATETIME NULL,
    IsDeleted BIT DEFAULT 0,

    CONSTRAINT FK_Experience_CvModel FOREIGN KEY (CvModelId) REFERENCES CvModel(Id)
);

CREATE TABLE Skills (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CvModelId INT NOT NULL,
    SkillName NVARCHAR(100),

    CreatedBy INT NULL,
    UpdatedBy INT NULL,
    DeletedBy INT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME NULL,
    DeletedDate DATETIME NULL,
    IsDeleted BIT DEFAULT 0,

    CONSTRAINT FK_Skill_CvModel FOREIGN KEY (CvModelId) REFERENCES CvModel(Id)
);
CREATE TABLE Certifications (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CvModelId INT NOT NULL,
    Title NVARCHAR(255),
    Organization NVARCHAR(255),
    IssueDate DATE,

    CreatedBy INT NULL,
    UpdatedBy INT NULL,
    DeletedBy INT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME NULL,
    DeletedDate DATETIME NULL,
    IsDeleted BIT DEFAULT 0,

    CONSTRAINT FK_Certification_CvModel FOREIGN KEY (CvModelId) REFERENCES CvModel(Id)
);
