﻿CREATE TABLE [dbo].[Customer_Data ] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [Address]     NVARCHAR (50) NOT NULL,
    [Pincode]     INT           NOT NULL,
    [Customer_No] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);