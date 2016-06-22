CREATE TABLE IF NOT EXISTS VolunteerTableV3(Id INT PRIMARY KEY AUTO_INCREMENT, UserIdentifier VARCHAR(15), Firstname NVARCHAR(20), Lastname NVARCHAR(20)
, Fathersname NVARCHAR(20), Gender NVARCHAR(5), Marriage NVARCHAR(10), CellPhone NVARCHAR(20), NationalCode NVARCHAR(20), BirthPlace NVARCHAR(20)
, BirthDate NVARCHAR(20), UniversityCourse NVARCHAR(30), UniversityPlace NVARCHAR(100), CourseDegree NVARCHAR(30), Level NVARCHAR(20)) ENGINE=INNODB;

SELECT UserIdentifier, Firstname, Lastname, Fathersname, Gender, Marriage, CellPhone, NationalCode, BirthPlace, BirthDate, UniversityCourse, UniversityPlace, CourseDegree FROM VolunteerTableV2;

SET character_set_results=utf8,character_set_client=utf8,character_set_connection=utf8, character_set_database=utf8,character_set_server=utf8;

INSERT INTO VolunteerTableV3 (UserIdentifier,Firstname,Lastname,Fathersname,Gender,Marriage,CellPhone,NationalCode,BirthPlace,BirthDate,UniversityCourse,UniversityPlace,CourseDegree,Level) SELECT UserIdentifier, Firstname, Lastname, Fathersname, Gender, Marriage, CellPhone, NationalCode, BirthPlace, BirthDate, UniversityCourse, UniversityPlace, CourseDegree, Level FROM VolunteerTableV2;

UPDATE VolunteerTableV3 SET Level = 2 Where level == 6;
UPDATE VolunteerTableV3 SET Level = 1 Where level != 2;