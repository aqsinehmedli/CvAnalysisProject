CREATE TABLE RefreshTokens (
    Id INT IDENTITY(1,1) PRIMARY KEY,  -- Avtomatik artan unikal ID
    Token NVARCHAR(500) NOT NULL,      -- Refresh Token dəyəri
    UserId INT NOT NULL,               -- İstifadəçi ID (Foreign Key)
    ExpirationDate DATETIME NOT NULL,  -- Token-in bitmə tarixi

    CONSTRAINT FK_RefreshTokens_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);
