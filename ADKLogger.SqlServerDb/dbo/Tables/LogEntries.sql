CREATE TABLE [dbo].[LogEntries] (
    [LogEntryId]  INT           IDENTITY (1, 1) NOT NULL,
    [Title]       VARCHAR (75)  NOT NULL,
    [Level]       VARCHAR (75)  NOT NULL,
    [Message]     VARCHAR (MAX) NOT NULL,
    [UserId]      VARCHAR (75)  NOT NULL,
    [DateCreated] DATETIME      NOT NULL,
    CONSTRAINT [PK_LogEntryId] PRIMARY KEY NONCLUSTERED ([LogEntryId] ASC)
);


GO
CREATE CLUSTERED INDEX [Idx_DateCreated]
    ON [dbo].[LogEntries]([DateCreated] ASC);


GO
CREATE NONCLUSTERED INDEX [Idx_UserId]
    ON [dbo].[LogEntries]([UserId] ASC);

