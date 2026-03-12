-- IAM DATABASE
CREATE DATABASE mcsp_identity;

-- APPLICATION DATABASE
CREATE DATABASE mcsp_app;

\c mcsp_app;

-- Schéma pour Entity Framework Core
CREATE SCHEMA IF NOT EXISTS efcore;

-- Schéma pour Marten (Event Sourcing & Document Store)
CREATE SCHEMA IF NOT EXISTS marten;

CREATE TABLE IF NOT EXISTS public.leads (
    id BIGSERIAL PRIMARY KEY,
    firstname TEXT NOT NULL,
    lastname TEXT NOT NULL,
    email TEXT UNIQUE NOT NULL,
    phone VARCHAR(20),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO public.leads (firstname, lastname, email, phone) VALUES
('Alice', 'Johnson', 'alice@example.com', '123-456-7890'),
('Bob', 'Smith', 'bob@example.com', '234-567-8901'),
('Charlie', 'Brown', 'charlie@example.com', '345-678-9012'),
('Diana', 'Prince', 'diana@example.com', '456-789-0123'),
('Ethan', 'Hunt', 'ethan@example.com', '567-890-1234')
ON CONFLICT (email) DO NOTHING;
