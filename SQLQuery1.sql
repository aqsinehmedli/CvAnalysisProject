ALTER TABLE Users ADD 
    IsDeleted BIT DEFAULT 0,
    BirthDate DATE NULL,
    CreatedBy NVARCHAR(255) NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    DeletedBy NVARCHAR(255) NULL,
    DeletedDate DATETIME NULL,
    FatherName NVARCHAR(255) NULL,
    Gender NVARCHAR(50) NULL,
    Location NVARCHAR(255) NULL,
    MobilePhone NVARCHAR(20) NULL,
    UpdatedBy NVARCHAR(255) NULL,
    UpdatedDate DATETIME NULL,
    UserRoles NVARCHAR(MAX) NULL;
