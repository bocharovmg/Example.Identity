CREATE PROCEDURE [user].[GetUserIdByLogin]
	@Login NVARCHAR(50)
AS
BEGIN
	DECLARE
		@UserAttributeSectionId_Name BIGINT = 1
		, @UserAttributeSectionId_Email BIGINT = 2


	SELECT TOP 1
		u_.Id
	FROM
		[user].Users u_
	JOIN
		[user].UserAttributes ua_name_ ON
			u_.Id = ua_name_.UserId
			AND ua_name_.UserAttributeSectionId IN (@UserAttributeSectionId_Name, @UserAttributeSectionId_Email)
	WHERE
		ua_name_.Value = @Login
END
