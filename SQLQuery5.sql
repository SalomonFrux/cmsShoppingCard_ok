
Create TABLE [dbo].[tblPages] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Title]      VARCHAR (50)  NULL,
    [Slug]       VARCHAR (50)  NULL,
    [Body]       VARCHAR (MAX) NULL,
    [Sorting]    INT           NULL,
    [HasSidebar] BIT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
select * from tblPages
