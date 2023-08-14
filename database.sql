CREATE DATABASE blogapp;

USE blogapp;


CREATE TABLE comments (
    comment_id INT PRIMARY KEY AUTO_INCREMENT,
    article_id INT NOT NULL,
    username VARCHAR(255) NOT NULL,
    text TEXT NOT NULL,
    post_date DATETIME NOT NULL
);

CREATE TABLE comment_replies (
    reply_id INT PRIMARY KEY AUTO_INCREMENT,
    comment_id INT NOT NULL,
    username VARCHAR(255) NOT NULL,
    text TEXT NOT NULL,
    post_date DATETIME NOT NULL
);

CREATE TABLE reactions (
    reaction_id INT PRIMARY KEY AUTO_INCREMENT,
    article_id INT NOT NULL,
    username VARCHAR(255) NOT NULL,
    reaction INT NOT NULL
);

CREATE TABLE articles (
    id INT PRIMARY KEY AUTO_INCREMENT,
    header VARCHAR(255) NOT NULL,
    about TEXT NOT NULL,
    title VARCHAR(255) NOT NULL,
    text TEXT NOT NULL,
    author VARCHAR(255) NOT NULL,
    post_date DATETIME NOT NULL,
    viewcount INT NOT NULL
);

CREATE TABLE Loged (
    _isAdmin BOOL NOT NULL,
    _username VARCHAR(255) NOT NULL,
    _login VARCHAR(255) NOT NULL,
    _password VARCHAR(255) NOT NULL
);