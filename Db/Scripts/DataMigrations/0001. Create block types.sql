/** BLOCK TYPES */

DECLARE
	@BlockTypeId_Permanent INT = 1
	, @BlockTypeDescription_Permanent NVARCHAR(50) = N'Permanent'
	, @BlockTypeId_Temporary INT = 2
	, @BlockTypeDescription_Temporary NVARCHAR(50) = N'Temporary'

SET IDENTITY_INSERT [user].BlockTypes ON;

INSERT INTO [user].BlockTypes
(
	Id
	, Description
)
VALUES
(
	@BlockTypeId_Permanent
	, @BlockTypeDescription_Permanent
),
(
	@BlockTypeId_Temporary
	, @BlockTypeDescription_Temporary
);

SET IDENTITY_INSERT [user].BlockTypes OFF;
