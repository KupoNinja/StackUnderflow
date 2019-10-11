-- Tables: Users, Questions, Categories, Responses, QuestionCategories

CREATE TABLE IF NOT EXISTS users (
    id VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    username VARCHAR(255) NOT NULL,
    hash VARCHAR(255) NOT NULL,

    PRIMARY KEY(id)
);

CREATE TABLE IF NOT EXISTS questions (
    id VARCHAR(255) NOT NULL,
    title VARCHAR(255) NOT NULL,
    body VARCHAR(255) NOT NULL,
    asked DATETIME NOT NULL,
    edited DATETIME,
    answered DATETIME,
    authorid VARCHAR(255) NOT NULL,
    answerid VARCHAR(255),

    PRIMARY KEY(id),
    FOREIGN KEY(authorid)
        REFERENCES users(id)
);

CREATE TABLE IF NOT EXISTS responses (
    id VARCHAR(255) NOT NULL,
    body VARCHAR(255) NOT NULL,
    replied DATETIME NOT NULL,
    edited DATETIME,
    questionid VARCHAR(255) NOT NULL,
    authorid VARCHAR(255) NOT NULL,

    PRIMARY KEY(id),
    FOREIGN KEY(questionid)
        REFERENCES questions(id),
    FOREIGN KEY(authorid)
        REFERENCES users(id)
);

ALTER TABLE questions
ADD FOREIGN KEY(answerid) 
    REFERENCES responses(id);

-- CREATE TABLE IF NOT EXISTS questionresponses (
--     id VARCHAR(255) NOT NULL,
--     questionid VARCHAR(255) NOT NULL,
--     responseid VARCHAR(255) NOT NULL,

--     PRIMARY KEY(id),
--     FOREIGN KEY(questionid)
--         REFERENCES questions(id),
--     FOREIGN KEY(responseid)
--         REFERENCES responses(id)
-- );

CREATE TABLE IF NOT EXISTS categories (
    id VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL UNIQUE,

    PRIMARY KEY(id)
);

CREATE TABLE IF NOT EXISTS questioncategories (
    id VARCHAR(255) NOT NULL,
    questionid VARCHAR(255) NOT NULL,
    categoryid VARCHAR(255) NOT NULL,

    PRIMARY KEY(id),
    FOREIGN KEY(questionid)
        REFERENCES questions(id),
    FOREIGN KEY(categoryid)
        REFERENCES categories(id)
);