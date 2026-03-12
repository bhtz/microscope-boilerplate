CREATE DATABASE mcsp_identity;
CREATE DATABASE mcsp_app;

\c mcsp_app;

-- Sample table exposed via Hasura
CREATE TABLE IF NOT EXISTS products (
    id BIGSERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    price NUMERIC(10,2) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Sample products
INSERT INTO products (name, price) VALUES
('Laptop', 1200.00),
('Keyboard', 80.00),
('Mouse', 40.00),
('Monitor', 300.00)
ON CONFLICT DO NOTHING;


-- Sample table exposed via Data API Builder
CREATE TABLE IF NOT EXISTS leads (
    id BIGSERIAL PRIMARY KEY,
    firstname TEXT NOT NULL,
    lastname TEXT NOT NULL,
    email TEXT UNIQUE NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Sample leads
INSERT INTO leads (firstname, lastname, email) VALUES
('Alice', 'Johnson', 'alice@example.com'),
('Bob', 'Smith', 'bob@example.com'),
('Charlie', 'Brown', 'charlie@example.com'),
('Diana', 'Prince', 'diana@example.com')
ON CONFLICT (email) DO NOTHING;
