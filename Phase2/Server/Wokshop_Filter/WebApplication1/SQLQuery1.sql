-- Create the database
CREATE DATABASE TodoAppNew4;
GO

-- Switch to the new database
USE TodoAppNew4;
GO


-- Insert data into TodoLists table
INSERT INTO TodoLists (Name) VALUES ('todo list1');
INSERT INTO TodoLists (Name) VALUES ('todo list2');
INSERT INTO TodoLists (Name) VALUES ('test');
INSERT INTO TodoLists (Name) VALUES ('test 2');
INSERT INTO TodoLists (Name) VALUES ('test 3');
INSERT INTO TodoLists (Name) VALUES ('list new');
INSERT INTO TodoLists (Name) VALUES ('list new 2');
INSERT INTO TodoLists (Name) VALUES ('list very new');
GO

-- Insert data into TodoTasks table
INSERT INTO TodoTasks (Name, IsStarred, IsCompleted, ListId, DueDate) VALUES
('task1 list1', 1, 1, 5, '2024-05-22 09:17:28'),
('task2 list1', 0, 0, 2, '2024-05-22 09:17:28'),
('tasks', 1, 1, 1, '1900-01-01 00:00:00'),
('test new', 1, 1, 2, '1900-01-06 00:00:00');
GO
SELECT * FROM TodoLists;

SELECT * FROM TodoTasks;

DROP DATABASE IF EXISTS [TodoAppNew3];
