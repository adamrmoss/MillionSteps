﻿CREATE TABLE dbo.ActivityLogEntry
(
  Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
  UserId NVARCHAR(1024) NOT NULL,
  [Date] DATETIME NOT NULL,
  Steps INT NOT NULL,
)
