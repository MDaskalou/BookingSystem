/*
SELECT * FROM Users WHERE UserId = 1;


INSERT INTO Users (Fullname, Email, PasswordHash, RoleId)
VALUES ('Test User', 'testuser@example.com', 'hashedpassword', 1); -- Justera värdena efter behov


INSERT INTO Roles (RoleName)
VALUES 
('User'),
('Manager'); -- Eller en annan roll som du vill använda

*/
/*
INSERT INTO Users (Fullname, Email, PasswordHash, RoleId)
VALUES ('Test User', 'testuser@example.com', 'hashedpassword', 1);  -- Använd en giltig RoleId här
*/

SELECT * FROM Users WHERE UserId = 1;
