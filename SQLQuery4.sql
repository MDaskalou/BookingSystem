/*
SELECT * FROM Users WHERE UserId = 1;


INSERT INTO Users (Fullname, Email, PasswordHash, RoleId)
VALUES ('Test User', 'testuser@example.com', 'hashedpassword', 1); -- Justera v�rdena efter behov


INSERT INTO Roles (RoleName)
VALUES 
('User'),
('Manager'); -- Eller en annan roll som du vill anv�nda

*/
/*
INSERT INTO Users (Fullname, Email, PasswordHash, RoleId)
VALUES ('Test User', 'testuser@example.com', 'hashedpassword', 1);  -- Anv�nd en giltig RoleId h�r
*/

SELECT * FROM Users WHERE UserId = 1;
