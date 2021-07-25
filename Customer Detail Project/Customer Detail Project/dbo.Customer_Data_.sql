CREATE TABLE [dbo].[CustomerData ] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    [Address]    NVARCHAR (50) NOT NULL,
    [Pincode]    NVARCHAR (8) NOT NULL,
    [Contact_No] NVARCHAR (16) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

