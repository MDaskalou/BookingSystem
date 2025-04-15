/*
CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(100) NOT NULL
);
*/
/*
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Fullname NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);
*/
/*
CREATE TABLE Patients (
    PatientId INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    SocialSecurityNumber NVARCHAR(50) NOT NULL
);
*/
/*
CREATE TABLE TreatmentTypes (
    TreatmentTypeId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);
*/
/*
CREATE TABLE Bookings (
    BookingId INT PRIMARY KEY IDENTITY(1,1),
    Date DATETIME NOT NULL,
    PatientId INT NOT NULL,
    TreatmentTypeId INT NOT NULL,
    CreatedById INT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    Priority INT NOT NULL, -- 0 = Green, 1 = Yellow, 2 = Red
    Status INT NOT NULL,   -- 0 = Pending, 1 = Confirmed, 2 = Cancelled
    FOREIGN KEY (PatientId) REFERENCES Patients(PatientId),
    FOREIGN KEY (TreatmentTypeId) REFERENCES TreatmentTypes(TreatmentTypeId),
    FOREIGN KEY (CreatedById) REFERENCES Users(UserId)
);
*/
/*
CREATE TABLE Documents (
    DocumentId INT PRIMARY KEY IDENTITY(1,1),
    FileName NVARCHAR(255) NOT NULL,
    Verified BIT NOT NULL DEFAULT 0,
    UploadedById INT NOT NULL,
    FOREIGN KEY (UploadedById) REFERENCES Users(UserId)
);
*/
/*
CREATE TABLE Notifications (
    NotificationId INT PRIMARY KEY IDENTITY(1,1),
    Message NVARCHAR(500) NOT NULL,
    SentAt DATETIME NOT NULL,
    RecipientId INT NOT NULL,
    FOREIGN KEY (RecipientId) REFERENCES Users(UserId)
);
*/


