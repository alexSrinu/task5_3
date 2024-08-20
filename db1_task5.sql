create database db1
use db1
CREATE TABLE tb_task5 (
    TaskId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Mobile NVARCHAR(20),
    Email NVARCHAR(30),
    StateId NVARCHAR(50),
    CityId NVARCHAR(50),
    StateName NVARCHAR(100),
    CityName NVARCHAR(100)
);
CREATE TABLE State (
    StateId NVARCHAR(50) PRIMARY KEY,
    StateName NVARCHAR(100) NOT NULL
);
CREATE TABLE City (
    CityId NVARCHAR(50) PRIMARY KEY,
    CityName NVARCHAR(100) NOT NULL,
    StateId NVARCHAR(50) NOT NULL,
    FOREIGN KEY (StateId) REFERENCES State(StateId)
);
CREATE PROCEDURE InsertTask
    @Name NVARCHAR(50),
    @Mobile NVARCHAR(20),
    @Email NVARCHAR(30),
    @StateId NVARCHAR(50),
    @CityId NVARCHAR(50)
AS
BEGIN
    DECLARE @CityName NVARCHAR(100);
    DECLARE @StateName NVARCHAR(100);

    -- Retrieve CityName based on CityId
    SELECT @CityName = CityName
    FROM City
    WHERE CityId = @CityId;

    -- Retrieve StateName based on StateId
    SELECT @StateName = StateName
    FROM State
    WHERE StateId = @StateId;

    -- Insert into tb_task5
    INSERT INTO tb_task5 (Name, Mobile, Email, StateId, CityId, StateName, CityName)
    VALUES (@Name, @Mobile, @Email, @StateId, @CityId, @StateName, @CityName);
END;
-- Insert into task5state with integer StateId
INSERT INTO State (StateId, StateName) VALUES
(1, 'Telangana'),
(2, 'AP'),
(3, 'Kerala'),
(4, 'TN');

-- Insert into task5city with integer CityId and StateId
INSERT INTO City (CityId, CityName, StateId) VALUES
(1, 'Hyderabad', 1),
(2, 'Warangal', 1),
(3, 'Nalgonda', 1),
(4, 'Khammam', 1),
(5, 'Amalapuram', 2),
(6, 'Vijayawada', 2),
(7, 'Guntur', 2),
(8, 'Rajahmundry', 2),
(9, 'Bapatla', 2),
(10, 'Visakhapatnam', 2),
(11, 'Kochi', 3),
(12, 'Kollam', 3),
(13, 'Kozhikode', 3),
(14, 'Thiruvananthapuram', 3),
(15, 'Chennai', 4),
(16, 'Coimbatore', 4),
(17, 'Madurai', 4),
(18, 'Tiruchirappalli', 4);
alter PROCEDURE PageTask
    @PageSize INT = 0,
    @PageNumber INT,
    @TotalCount INT OUT
AS
BEGIN
    SET NOCOUNT ON;
 IF @PageSize = 0 OR @PageSize IS NULL
    BEGIN
        SET @PageSize = 2;
    END
 DECLARE @Offset INT;
    SET @Offset = (@PageNumber - 1) * @PageSize;
 SELECT @TotalCount = COUNT(*)
    FROM  tb_task5
 SELECT *
    FROM tb_task5
    ORDER BY Name
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
declare @totalcount int
exec [dbo].[PageTask]
@PageSize=3,@PageNumber=1,@totalcount=@totalcount output;
select @totalcount as totalcount;

CREATE PROCEDURE PROC_CHECKBOX1
@StateName varchar(50)
as begin
select * from tb_task5 where statename=@statename
end
alter PROCEDURE UpdateTask
    @Id INT,
    @Name NVARCHAR(50),
    @Mobile NVARCHAR(20),
    @Email NVARCHAR(30),
	 @StateId NVARCHAR(50),
    @CityId NVARCHAR(50),
    @StateName NVARCHAR(100),
    @CityName NVARCHAR(100)
AS
BEGIN
    UPDATE tb_task5
    SET
        Name = @Name,
        Mobile = @Mobile,
        Email = @Email,
		CityId=@CityId,
		StateId=@StateId,
        StateName=@StateName,
        CityName=@CityName
    WHERE TaskId = @Id;
END
EXEC UpdateTask 
    @Id = 1,                
    @Name = 'alekhya',    
    @Mobile = '9874561230', 
    @Email = 'alex@example.com', 
    @StateName = 'AP', 
    @CityName = 'Bapatla'; 
alter procedure selecttask2(@id int)
as begin
select TaskId,Name,Mobile,Email,StateName,CityName from tb_task5 where(TaskId=@id)
end
exec selecttask2 1
create procedure selecttask1
as begin
select * from tb_task5
end
CREATE PROCEDURE PROC_CHECKBOX5
    @StateName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT c.CityName
    FROM City c
    INNER JOIN State s ON c.StateId = s.StateId
    WHERE s.StateName = @StateName;
END;
create procedure proc_delete(@id int)
as begin
delete from tb_task5 where TaskId=@id
END
alter PROCEDURE UpdateTask
    @Id INT,
    @Name NVARCHAR(50),
    @Mobile NVARCHAR(20),
    @Email NVARCHAR(30),
    @StateId NVARCHAR(50),
    @CityId NVARCHAR(50)
AS
BEGIN
    DECLARE @CityName NVARCHAR(100);
    DECLARE @StateName NVARCHAR(100);

    
    SELECT @CityName = CityName
    FROM City
    WHERE CityId = @CityId;

   
    SELECT @StateName = StateName
    FROM State
    WHERE StateId = @StateId;

  
    UPDATE tb_task5
    SET 
        Name = @Name,
        Mobile = @Mobile,
        Email = @Email,
        StateId = @StateId,
        CityId = @CityId,
        StateName = @StateName,
        CityName = @CityName
    WHERE TaskId = @Id;
END;
