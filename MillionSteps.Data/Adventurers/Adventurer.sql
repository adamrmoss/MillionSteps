CREATE TABLE dbo.Adventurer
(
  Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
  UserId NVARCHAR(32) NOT NULL,
  DateCreated DATETIME NOT NULL,
  Name NVARCHAR(64) NULL,
  Gender NVARCHAR(6) NULL,
  Strength INT NULL,
  Dexterity INT NULL,
  Constitution INT NULL,
  Intelligence INT NULL,
  Wisdom INT NULL,
  Charisma INT NULL,
)
