CREATE TABLE [dbo].[Tier] (
    [Id]               INT  IDENTITY (1, 1) NOT NULL,
    [tier_name] TEXT NULL,
    [points_needed]    INT  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);