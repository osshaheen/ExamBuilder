namespace ExamBuilder.Domain.Enums;

public enum UserStatus
{
    Active,
    Pending,
    Inactive
}

public enum YearStatus
{
    Active,
    Archived
}

public enum ExamStatus
{
    Draft,
    Ready
}

public enum QuestionLayout
{
    Vertical,
    Horizontal
}

public enum ActivityAction
{
    Create,
    Update,
    Delete,
    Export,
    Login
}
